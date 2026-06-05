using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Liberator;

public partial class MainForm : Form
{

    public MainForm()
    {
        InitializeComponent();
        _comboMadHouse.SelectedIndex = 0;
        this.Text = "Liberator - " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        LoadRegistry();
    }

    private void SaveRegistry()
    {
        RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Throwback\\Liberator");
        registryKey.SetValue("LastR6SPID", _r6sPid.ToString());
    }

    private void LoadRegistry()
    {
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Throwback\\Liberator");
        if (registryKey != null)
        {
            _r6sPid = Convert.ToInt32(registryKey.GetValue("LastR6SPID"));
        }
    }

    private void OnAttachTick(object sender, EventArgs e)
    {
        _r6s.Reattach();
        if (_r6s.IsAttached())
        {
            _gameBuild = _r6s.DetectBuild();
            if (_gameBuild == GameBuild.None)
            {
                ClearGametypes();
                _pendingAttach = true;
                SetStatus("This game build is not supported", false);
                _modsInitialized = false;
                return;
            }
            if (!_pendingAttach)
            {
                if (_countdown == 0)
                {
                    if (!_modsInitialized)
                    {
                        _r6sPid = _r6s.KillGuardThread(_r6sPid);
                        SaveRegistry();
                        _modsInitialized = true;
                    }
                    ApplyMods();
                    return;
                }
                if (_countdown != 5)
                {
                    _countdown--;
                }
            }
            else
            {
                if (!_r6s.IsFullyLaunched())
                {
                    SetStatus("Waiting for R6S to fully launch", false);
                    _countdown = 5;
                    return;
                }
                _gameBuild = _r6s.DetectBuild();
                SetStatus("Attached to " + R6S.SeasonNames[(int)_r6s.GetSeason()] + " (v" + _r6s.GetBuildVersion(_gameBuild) + ")", false);
                _selectedGametypeId = 0L;
                PopulateGametypes();
                _pendingAttach = false;
                if (_countdown == 5)
                {
                    _countdown--;
                    return;
                }
            }
        }
        else
        {
            ClearGametypes();
            _pendingAttach = true;
            SetStatus("Waiting for R6S to launch — make sure BattlEye is disabled", false);
            _modsInitialized = false;
        }
    }

    private void SetStatus(string message, bool idle)
    {
        _toolStripStatusLabel1.Text = message;
        if (!idle)
        {
            _statusHoldTicks = 5;
        }
    }

    private void ApplyMods()
    {
        if (_r6s.GetSeason() < Season.Y2S1_Velvet_Shell)
        {
            _checkBoxHarvard.Enabled = true;
            _r6s.SetHarvard(_checkBoxHarvard.Checked);
        }
        else
        {
            _checkBoxHarvard.Enabled = false;
        }
        if (_gameBuild != GameBuild.Y4S2_13147883)
        {
            _checkBoxOldHereford.Enabled = false;
        }
        else
        {
            _checkBoxOldHereford.Enabled = true;
            _r6s.SetOldHereford(_checkBoxOldHereford.Checked);
        }
        if (_gameBuild == GameBuild.Y3S3_12362767)
        {
            _comboMadHouse.Visible = true;
            _labelMadHouse.Visible = true;
            _r6s.SetMadHouseMode(_comboMadHouse.SelectedIndex);
        }
        else
        {
            _labelMadHouse.Visible = false;
            _comboMadHouse.Visible = false;
        }
        if (_r6s.GetSeason() >= Season.Y2S4_White_Noise)
        {
            _checkBoxDisplayVer.Enabled = true;
            _r6s.SetDisplayVersion(_checkBoxDisplayVer.Checked);
            if (_checkBoxDisplayVer.Checked && _buildLabelHintPending)
            {
                _buildLabelHintPending = false;
                MessageBox.Show("You will need to change your video resolution for the build label to appear.", "Build Display");
            }
        }
        else
        {
            _checkBoxDisplayVer.Enabled = false;
        }
        _r6s.ApplyCorePatch(true);
        if (_checkBoxDisablePrimaryWeapon.Checked && _checkBoxDisableSecondaryWeapon.Checked && _r6s.GetSeason() < Season.Y2S3_Blood_Orchid)
        {
            _r6s.SetEmptySecondary(true);
            _r6s.SetDisablePrimaryWeapon(_checkBoxDisablePrimaryWeapon.Checked);
            if (_secondaryWeaponHintPending)
            {
                _secondaryWeaponHintPending = false;
                MessageBox.Show("Seasons before Blood Orchid will not let players move without at least one weapon, so we will give you a secondary weapon with no ammo.");
            }
        }
        else
        {
            _r6s.SetEmptySecondary(false);
            _r6s.SetDisablePrimaryWeapon(_checkBoxDisablePrimaryWeapon.Checked);
            _r6s.SetDisableSecondaryWeapon(_checkBoxDisableSecondaryWeapon.Checked);
        }
        _r6s.SetSpecialAbility(_checkBoxSpecial.Checked);
        _r6s.SetDisableGadget(_checkBoxDisableGadget.Checked);
        _r6s.SetUnlimitedEquipment(_checkBoxUnlimitedEquip.Checked);
        _r6s.SetGodMode(_checkBoxGodMode.Checked);
        _r6s.SetDisableAI(_checkBoxDisableAI.Checked);
        _r6s.SetBottomlessMagazine(_checkBoxBottomless.Checked);
        if ((string)_checkBoxInfTime.Tag != "1")
        {
            _checkBoxInfTime.Checked = _r6s.GetInfiniteTime();
        }
        else
        {
            _r6s.SetInfiniteTime(_checkBoxInfTime.Checked);
            _checkBoxInfTime.Tag = "0";
        }
        if (_selectedGametypeId != 0L)
        {
            _r6s.SetGametype(_selectedGametypeId);
        }
        if (_r6s.GetSeason() < Season.Y1S2_Dust_Line)
        {
            _button1.Enabled = false;
            _button2.Enabled = false;
            return;
        }
        _button1.Enabled = true;
        _button2.Enabled = true;
    }

    private void ClearGametypes()
    {
        _treeView1.Nodes.Clear();
    }

    private void PopulateGametypes()
    {
        _treeView1.Nodes.Clear();
        _gametypeSource = _r6s.BuildGametypeTree();
        int num = 0;
        int num2 = 0;
        int num3 = 0;
        int num4 = 0;
        IEnumerator enumerator = _gametypeSource.Nodes.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                TreeNode treeNode = (TreeNode)enumerator.Current;
                _treeView1.Nodes.Add((string)treeNode.Tag, treeNode.Text);
                IEnumerator enumerator2 = _gametypeSource.Nodes[num].Nodes.GetEnumerator();
                try
                {
                    while (enumerator2.MoveNext())
                    {
                        TreeNode treeNode2 = (TreeNode)enumerator2.Current;
                        _treeView1.Nodes[num].Nodes.Add((string)treeNode2.Tag, treeNode2.Text);
                        IEnumerator enumerator3 = _gametypeSource.Nodes[num].Nodes[num2].Nodes.GetEnumerator();
                        try
                        {
                            while (enumerator3.MoveNext())
                            {
                                TreeNode treeNode3 = (TreeNode)enumerator3.Current;
                                _treeView1.Nodes[num].Nodes[num2].Nodes.Add((string)treeNode3.Tag, treeNode3.Text);
                                IEnumerator enumerator4 = _gametypeSource.Nodes[num].Nodes[num2].Nodes[num3].Nodes.GetEnumerator();
                                try
                                {
                                    while (enumerator4.MoveNext())
                                    {
                                        TreeNode treeNode4 = (TreeNode)enumerator4.Current;
                                        _treeView1.Nodes[num].Nodes[num2].Nodes[num3].Nodes.Add((string)treeNode4.Tag, treeNode4.Text);
                                        IEnumerator enumerator5 = _gametypeSource.Nodes[num].Nodes[num2].Nodes[num3].Nodes[num4].Nodes.GetEnumerator();
                                        try
                                        {
                                            while (enumerator5.MoveNext())
                                            {
                                                TreeNode treeNode5 = (TreeNode)enumerator5.Current;
                                                _treeView1.Nodes[num].Nodes[num2].Nodes[num3].Nodes[num4].Nodes.Add((string)treeNode5.Tag, treeNode5.Text);
                                            }
                                        }
                                        finally
                                        {
                                            IDisposable disposable = enumerator5 as IDisposable;
                                            if (disposable != null)
                                            {
                                                disposable.Dispose();
                                            }
                                        }
                                        num4++;
                                    }
                                }
                                finally
                                {
                                    IDisposable disposable = enumerator4 as IDisposable;
                                    if (disposable != null)
                                    {
                                        disposable.Dispose();
                                    }
                                }
                                num3++;
                                num4 = 0;
                            }
                        }
                        finally
                        {
                            IDisposable disposable = enumerator3 as IDisposable;
                            if (disposable != null)
                            {
                                disposable.Dispose();
                            }
                        }
                        num2++;
                        num3 = 0;
                    }
                }
                finally
                {
                    IDisposable disposable = enumerator2 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
                num++;
                num2 = 0;
            }
        }
        finally
        {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }

    private void OnGametypeSelected(object sender, EventArgs e)
    {
        try
        {
            if (_treeView1.SelectedNode.Nodes.Count == 0 && _r6s.IsAttached())
            {
                long gametypeId = Convert.ToInt64(_treeView1.SelectedNode.Name);
                _r6s.SetGametype(gametypeId);
                _selectedGametypeId = gametypeId;
                if ((string)_treeView1.Tag != _treeView1.SelectedNode.Name)
                {
                    SetStatus("Gametype set: " + _treeView1.SelectedNode.FullPath, false);
                    _treeView1.Tag = _treeView1.SelectedNode.Name;
                }
            }
        }
        catch
        {
        }
    }

    private void OnAboutLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start((string)e.Link.LinkData);
    }

    private void OnInfiniteTimeToggled(object sender, EventArgs e)
    {
        (sender as CheckBox).Tag = "1";
    }

    private void OnButton1Click(object sender, EventArgs e)
    {
        _r6s.EndRound();
    }

    private void OnButton2Click(object sender, EventArgs e)
    {
        _r6s.EndMatch();
    }

    private void OnStatusTick(object sender, EventArgs e)
    {
        if (_statusHoldTicks > 0)
        {
            _statusHoldTicks--;
        }
        if (_statusHoldTicks == 0)
        {
            SetStatus("Idle", true);
            _statusHoldTicks = -1;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && _components != null)
        {
            _components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _components = new Container();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(MainForm));
        _statusStrip1 = new StatusStrip();
        _toolStripStatusLabel1 = new ToolStripStatusLabel();
        _attachTimer = new Timer(_components);
        _treeView1 = new TreeView();
        _label1 = new Label();
        _mainTabs = new FocuslessTabControl();
        _tabGametypes = new TabPage();
        _comboMadHouse = new ComboBox();
        _labelMadHouse = new Label();
        _tabMods = new TabPage();
        _groupBox3 = new GroupBox();
        _button2 = new Button();
        _button1 = new Button();
        _checkBoxDisableAI = new CheckBox();
        _checkBoxInfTime = new CheckBox();
        _groupBox2 = new GroupBox();
        _groupBoxPlayers = new GroupBox();
        _groupBoxMap = new GroupBox();
        _groupBoxDisplay = new GroupBox();
        _checkBoxSpecial = new CheckBox();
        _checkBoxDisableGadget = new CheckBox();
        _checkBoxDisablePrimaryWeapon = new CheckBox();
        _checkBoxDisableSecondaryWeapon = new CheckBox();
        _checkBoxOldHereford = new CheckBox();
        _checkBoxDisplayVer = new CheckBox();
        _checkBoxBottomless = new CheckBox();
        _checkBoxHarvard = new CheckBox();
        _checkBoxGodMode = new CheckBox();
        _checkBoxUnlimitedEquip = new CheckBox();
        _tabAbout = new TabPage();
        _label6 = new LinkLabel();
        _groupBox1 = new GroupBox();
        _listViewLeft = new ListView();
        _listViewRight = new ListView();
        _columnSeasonLeft = new ColumnHeader();
        _columnBuildLeft = new ColumnHeader();
        _columnSeasonRight = new ColumnHeader();
        _columnBuildRight = new ColumnHeader();
        _toolTip1 = new ToolTip(_components);
        _statusTimer = new Timer(_components);
        _statusStrip1.SuspendLayout();
        _mainTabs.SuspendLayout();
        _tabGametypes.SuspendLayout();
        _tabMods.SuspendLayout();
        _groupBox3.SuspendLayout();
        _groupBox2.SuspendLayout();
        _groupBoxPlayers.SuspendLayout();
        _groupBoxMap.SuspendLayout();
        _groupBoxDisplay.SuspendLayout();
        _tabAbout.SuspendLayout();
        _groupBox1.SuspendLayout();
        this.SuspendLayout();
        _statusStrip1.Items.AddRange(new ToolStripItem[] { _toolStripStatusLabel1 });
        _statusStrip1.Location = new Point(0, 523);
        _statusStrip1.Name = "statusStrip1";
        _statusStrip1.Size = new Size(615, 22);
        _statusStrip1.SizingGrip = false;
        _statusStrip1.TabIndex = 1;
        _statusStrip1.Text = "statusStrip1";
        _toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        _toolStripStatusLabel1.Size = new Size(600, 17);
        _toolStripStatusLabel1.Spring = true;
        _toolStripStatusLabel1.Text = "Idle";
        _attachTimer.Enabled = true;
        _attachTimer.Interval = 1000;
        _attachTimer.Tick += new EventHandler(OnAttachTick);
        _treeView1.Location = new Point(12, 38);
        _treeView1.Name = "treeView1";
        _treeView1.Size = new Size(568, 403);
        _treeView1.TabIndex = 9;
        _toolTip1.SetToolTip(_treeView1, "Double-click the desired gametype to activate it. Make sure you are in a custom game lobby.");
        _treeView1.Click += new EventHandler(OnGametypeSelected);
        _label1.AutoSize = true;
        _label1.Location = new Point(12, 9);
        _label1.Name = "label1";
        _label1.Size = new Size(330, 26);
        _label1.TabIndex = 12;
        _label1.Text = "Start a custom game, then double-click the desired gametype.\r\nPvE crashes on any build after 1.0 if anyone is on the orange team.";
        _mainTabs.Controls.Add(_tabGametypes);
        _mainTabs.Controls.Add(_tabMods);
        _mainTabs.Controls.Add(_tabAbout);
        _mainTabs.Location = new Point(12, 12);
        _mainTabs.Name = "mainTabs";
        _mainTabs.SelectedIndex = 0;
        _mainTabs.Size = new Size(600, 508);
        _mainTabs.TabIndex = 17;
        _tabGametypes.Controls.Add(_comboMadHouse);
        _tabGametypes.Controls.Add(_labelMadHouse);
        _tabGametypes.Controls.Add(_label1);
        _tabGametypes.Controls.Add(_treeView1);
        _tabGametypes.Location = new Point(4, 22);
        _tabGametypes.Name = "tabGametypes";
        _tabGametypes.Padding = new Padding(3);
        _tabGametypes.Size = new Size(592, 482);
        _tabGametypes.TabIndex = 0;
        _tabGametypes.Text = "Gametypes";
        _tabGametypes.UseVisualStyleBackColor = true;
        _comboMadHouse.DropDownStyle = ComboBoxStyle.DropDownList;
        _comboMadHouse.FormattingEnabled = true;
        _comboMadHouse.Items.AddRange(new object[] { "Mad House", "Hostage", "Secure", "Bomb", "Warmup", "Canister", "Gym" });
        _comboMadHouse.Location = new Point(295, 447);
        _comboMadHouse.Name = "comboMadHouse";
        _comboMadHouse.Size = new Size(121, 21);
        _comboMadHouse.TabIndex = 19;
        _comboMadHouse.Visible = false;
        _labelMadHouse.AutoSize = true;
        _labelMadHouse.Location = new Point(176, 450);
        _labelMadHouse.Name = "labelMadHouse";
        _labelMadHouse.Size = new Size(113, 13);
        _labelMadHouse.TabIndex = 18;
        _labelMadHouse.Text = "Mad House Gametype";
        _labelMadHouse.Visible = false;
        _tabMods.Controls.Add(_groupBoxPlayers);
        _tabMods.Controls.Add(_groupBoxMap);
        _tabMods.Controls.Add(_groupBoxDisplay);
        _tabMods.Controls.Add(_groupBox2);
        _tabMods.Controls.Add(_groupBox3);
        _tabMods.Location = new Point(4, 22);
        _tabMods.Name = "tabMods";
        _tabMods.Padding = new Padding(3);
        _tabMods.Size = new Size(592, 482);
        _tabMods.TabIndex = 1;
        _tabMods.Text = "Modifications";
        _tabMods.UseVisualStyleBackColor = true;
        _groupBox3.Controls.Add(_button2);
        _groupBox3.Controls.Add(_button1);
        _groupBox3.Controls.Add(_checkBoxDisableAI);
        _groupBox3.Controls.Add(_checkBoxInfTime);
        _groupBox3.Location = new Point(304, 151);
        _groupBox3.Name = "groupBox3";
        _groupBox3.Size = new Size(276, 106);
        _groupBox3.TabIndex = 28;
        _groupBox3.TabStop = false;
        _groupBox3.Text = "Match";
        _button2.Location = new Point(92, 19);
        _button2.Name = "button2";
        _button2.Size = new Size(75, 23);
        _button2.TabIndex = 3;
        _button2.Text = "End Match";
        _button2.UseVisualStyleBackColor = true;
        _button2.Click += new EventHandler(OnButton2Click);
        _button1.Location = new Point(11, 19);
        _button1.Name = "button1";
        _button1.Size = new Size(75, 23);
        _button1.TabIndex = 2;
        _button1.Text = "End Round";
        _button1.UseVisualStyleBackColor = true;
        _button1.Click += new EventHandler(OnButton1Click);
        _checkBoxDisableAI.AutoSize = true;
        _checkBoxDisableAI.Location = new Point(11, 71);
        _checkBoxDisableAI.Name = "checkBoxDisableAI";
        _checkBoxDisableAI.Size = new Size(203, 17);
        _checkBoxDisableAI.TabIndex = 1;
        _checkBoxDisableAI.Text = "Brain-Dead AI (set before match start)";
        _checkBoxDisableAI.UseVisualStyleBackColor = true;
        _checkBoxInfTime.AutoSize = true;
        _checkBoxInfTime.Location = new Point(11, 48);
        _checkBoxInfTime.Name = "checkBoxInfTime";
        _checkBoxInfTime.Size = new Size(116, 17);
        _checkBoxInfTime.TabIndex = 0;
        _checkBoxInfTime.Tag = "0";
        _checkBoxInfTime.Text = "Infinite Match Time";
        _checkBoxInfTime.UseVisualStyleBackColor = true;
        _checkBoxInfTime.Click += new EventHandler(OnInfiniteTimeToggled);
        _groupBox2.Controls.Add(_checkBoxSpecial);
        _groupBox2.Controls.Add(_checkBoxDisableGadget);
        _groupBox2.Controls.Add(_checkBoxDisablePrimaryWeapon);
        _groupBox2.Controls.Add(_checkBoxDisableSecondaryWeapon);
        _groupBox2.Location = new Point(304, 14);
        _groupBox2.Name = "groupBox2";
        _groupBox2.Size = new Size(276, 123);
        _groupBox2.TabIndex = 27;
        _groupBox2.TabStop = false;
        _groupBox2.Text = "Loadout (set before spawn)";
        _checkBoxSpecial.AutoSize = true;
        _checkBoxSpecial.Location = new Point(11, 65);
        _checkBoxSpecial.Name = "checkBoxSpecial";
        _checkBoxSpecial.Size = new Size(137, 17);
        _checkBoxSpecial.TabIndex = 25;
        _checkBoxSpecial.Text = "Disable Special Gadget";
        _checkBoxSpecial.UseVisualStyleBackColor = true;
        _checkBoxDisableGadget.AutoSize = true;
        _checkBoxDisableGadget.Location = new Point(11, 88);
        _checkBoxDisableGadget.Name = "checkBoxDisableGadget";
        _checkBoxDisableGadget.Size = new Size(99, 17);
        _checkBoxDisableGadget.TabIndex = 26;
        _checkBoxDisableGadget.Text = "Disable Gadget";
        _checkBoxDisableGadget.UseVisualStyleBackColor = true;
        _checkBoxDisablePrimaryWeapon.AutoSize = true;
        _checkBoxDisablePrimaryWeapon.Location = new Point(11, 19);
        _checkBoxDisablePrimaryWeapon.Name = "checkBoxDisablePrimaryWeapon";
        _checkBoxDisablePrimaryWeapon.Size = new Size(142, 17);
        _checkBoxDisablePrimaryWeapon.TabIndex = 23;
        _checkBoxDisablePrimaryWeapon.Text = "Disable Primary Weapon";
        _checkBoxDisablePrimaryWeapon.UseVisualStyleBackColor = true;
        _checkBoxDisableSecondaryWeapon.AutoSize = true;
        _checkBoxDisableSecondaryWeapon.Location = new Point(11, 42);
        _checkBoxDisableSecondaryWeapon.Name = "checkBoxDisableSecondaryWeapon";
        _checkBoxDisableSecondaryWeapon.Size = new Size(159, 17);
        _checkBoxDisableSecondaryWeapon.TabIndex = 24;
        _checkBoxDisableSecondaryWeapon.Text = "Disable Secondary Weapon";
        _checkBoxDisableSecondaryWeapon.UseVisualStyleBackColor = true;
        _groupBoxPlayers.Controls.Add(_checkBoxGodMode);
        _groupBoxPlayers.Controls.Add(_checkBoxUnlimitedEquip);
        _groupBoxPlayers.Controls.Add(_checkBoxBottomless);
        _groupBoxPlayers.Location = new Point(12, 14);
        _groupBoxPlayers.Name = "groupBoxPlayers";
        _groupBoxPlayers.Size = new Size(276, 95);
        _groupBoxPlayers.TabStop = false;
        _groupBoxPlayers.Text = "Players";
        _groupBoxMap.Controls.Add(_checkBoxHarvard);
        _groupBoxMap.Controls.Add(_checkBoxOldHereford);
        _groupBoxMap.Location = new Point(12, 123);
        _groupBoxMap.Name = "groupBoxMap";
        _groupBoxMap.Size = new Size(276, 72);
        _groupBoxMap.TabStop = false;
        _groupBoxMap.Text = "Map";
        _groupBoxDisplay.Controls.Add(_checkBoxDisplayVer);
        _groupBoxDisplay.Location = new Point(12, 209);
        _groupBoxDisplay.Name = "groupBoxDisplay";
        _groupBoxDisplay.Size = new Size(276, 48);
        _groupBoxDisplay.TabStop = false;
        _groupBoxDisplay.Text = "Display";
        _checkBoxOldHereford.AutoSize = true;
        _checkBoxOldHereford.Location = new Point(11, 42);
        _checkBoxOldHereford.Name = "checkBoxOldHereford";
        _checkBoxOldHereford.Size = new Size(141, 17);
        _checkBoxOldHereford.TabIndex = 22;
        _checkBoxOldHereford.Text = "Enable Original Hereford";
        _toolTip1.SetToolTip(_checkBoxOldHereford, "Replaces the Hereford rework with the original version.\r\nHost authority.");
        _checkBoxOldHereford.UseVisualStyleBackColor = true;
        _checkBoxDisplayVer.AutoSize = true;
        _checkBoxDisplayVer.Location = new Point(11, 19);
        _checkBoxDisplayVer.Name = "checkBoxDisplayVer";
        _checkBoxDisplayVer.Size = new Size(124, 17);
        _checkBoxDisplayVer.TabIndex = 21;
        _checkBoxDisplayVer.Text = "Build Number";
        _toolTip1.SetToolTip(_checkBoxDisplayVer, componentResourceManager.GetString("checkBoxDisplayVer.ToolTip"));
        _checkBoxDisplayVer.UseVisualStyleBackColor = true;
        _checkBoxBottomless.AutoSize = true;
        _checkBoxBottomless.Location = new Point(11, 65);
        _checkBoxBottomless.Name = "checkBoxBottomless";
        _checkBoxBottomless.Size = new Size(164, 17);
        _checkBoxBottomless.TabIndex = 20;
        _checkBoxBottomless.Text = "Bottomless Clip (Host, buggy)";
        _toolTip1.SetToolTip(_checkBoxBottomless, "Gives all players a ton of ammo so they never have to reload.\r\nYou must enable this before spawning.\r\nHost authority.");
        _checkBoxBottomless.UseVisualStyleBackColor = true;
        _checkBoxHarvard.AutoSize = true;
        _checkBoxHarvard.Location = new Point(11, 19);
        _checkBoxHarvard.Name = "checkBoxHarvard";
        _checkBoxHarvard.Size = new Size(238, 17);
        _checkBoxHarvard.TabIndex = 17;
        _checkBoxHarvard.Text = "Replace House with Bartlett University (Host)";
        _toolTip1.SetToolTip(_checkBoxHarvard, "Create a playlist with House and when you start the match it will load up Bartlett University instead.\r\nHost authority.");
        _checkBoxHarvard.UseVisualStyleBackColor = true;
        _checkBoxGodMode.AutoSize = true;
        _checkBoxGodMode.Location = new Point(11, 19);
        _checkBoxGodMode.Name = "checkBoxGodMode";
        _checkBoxGodMode.Size = new Size(205, 17);
        _checkBoxGodMode.TabIndex = 19;
        _checkBoxGodMode.Text = "Deathless Players and Hostage (Host)";
        _toolTip1.SetToolTip(_checkBoxGodMode, "Prevents all players and the hostage from dying from most forms of damage.\r\nHost authority.");
        _checkBoxGodMode.UseVisualStyleBackColor = true;
        _checkBoxUnlimitedEquip.AutoSize = true;
        _checkBoxUnlimitedEquip.Location = new Point(11, 42);
        _checkBoxUnlimitedEquip.Name = "checkBoxUnlimitedEquip";
        _checkBoxUnlimitedEquip.Size = new Size(153, 17);
        _checkBoxUnlimitedEquip.TabIndex = 18;
        _checkBoxUnlimitedEquip.Text = "Unlimited Equipment (Host)";
        _toolTip1.SetToolTip(_checkBoxUnlimitedEquip, "All players will have unlimited equipment and reinforcements.\r\nHost authority.");
        _checkBoxUnlimitedEquip.UseVisualStyleBackColor = true;
        _tabAbout.Controls.Add(_label6);
        _tabAbout.Controls.Add(_groupBox1);
        _tabAbout.Location = new Point(4, 22);
        _tabAbout.Name = "tabAbout";
        _tabAbout.Padding = new Padding(3);
        _tabAbout.Size = new Size(592, 482);
        _tabAbout.TabIndex = 3;
        _tabAbout.Text = "About";
        _tabAbout.UseVisualStyleBackColor = true;
        _label6.AutoSize = false;
        _label6.Font = new Font("Microsoft Sans Serif", (float)(8.25f), FontStyle.Regular, GraphicsUnit.Point, 0);
        _label6.Location = new Point(12, 9);
        _label6.Name = "label6";
        _label6.Size = new Size(488, 140);
        _label6.TabIndex = 1;
        _label6.Text = string.Join("\r\n", new string[] { "This is a further development of Liberator for Operation Throwback. The original developer was Gamecheat13.", "", "Liberator unlocks extra possibilities in Tom Clancy's Rainbow Six\u00AE Siege local custom games. It cannot be used to cheat on live servers, and never will be.", "", "Join the R6S: Operation Throwback 2.0 Discord server for updates and help." });
        _label6.Links.Clear();
        _label6.Links.Add(_label6.Text.IndexOf("R6S: Operation Throwback 2.0"), "R6S: Operation Throwback 2.0".Length, "https://discord.com/invite/r6s-operation-throwback-2-0-1092820800203141130");
        _label6.LinkClicked += new LinkLabelLinkClickedEventHandler(OnAboutLinkClicked);
        _groupBox1.Controls.Add(_listViewLeft);
        _groupBox1.Controls.Add(_listViewRight);
        _groupBox1.Location = new Point(12, 188);
        _groupBox1.Name = "groupBox1";
        _groupBox1.Size = new Size(568, 291);
        _groupBox1.TabIndex = 0;
        _groupBox1.TabStop = false;
        _groupBox1.Text = "Support";
        _listViewLeft.BorderStyle = BorderStyle.None;
        _listViewLeft.Columns.AddRange(new ColumnHeader[] { _columnSeasonLeft, _columnBuildLeft });
        _listViewLeft.FullRowSelect = true;
        _listViewLeft.GridLines = true;
        _listViewLeft.HeaderStyle = ColumnHeaderStyle.None;
        _listViewLeft.Location = new Point(7, 19);
        _listViewLeft.MultiSelect = false;
        _listViewLeft.Name = "listViewLeft";
        _listViewLeft.ShowItemToolTips = true;
        _listViewLeft.Size = new Size(190, 266);
        _listViewLeft.TabIndex = 1;
        _listViewLeft.UseCompatibleStateImageBehavior = false;
        _listViewLeft.View = View.Details;
        _listViewLeft.Items.AddRange(new ListViewItem[] {
            new ListViewItem(new string[] { "Y1S0 Vanilla", "v8194013" }) { ToolTipText = "Y1S0 Vanilla - v8194013" },
            new ListViewItem(new string[] { "Y1S1 Black Ice", "v8519860" }) { ToolTipText = "Y1S1 Black Ice - v8519860" },
            new ListViewItem(new string[] { "Y1S2 Dust Line", "v9132097" }) { ToolTipText = "Y1S2 Dust Line - v9132097" },
            new ListViewItem(new string[] { "Y1S3 Skull Rain", "v9654076" }) { ToolTipText = "Y1S3 Skull Rain - v9654076" },
            new ListViewItem(new string[] { "Y1S3 Skull Rain", "v9860556" }) { ToolTipText = "Y1S3 Skull Rain - v9860556" },
            new ListViewItem(new string[] { "Y1S4 Red Crow", "v10211195" }) { ToolTipText = "Y1S4 Red Crow - v10211195" },
            new ListViewItem(new string[] { "Y2S1 Velvet Shell", "v10751226" }) { ToolTipText = "Y2S1 Velvet Shell - v10751226" },
            new ListViewItem(new string[] { "Y2S2 Health", "v11216230" }) { ToolTipText = "Y2S2 Health - v11216230" },
            new ListViewItem(new string[] { "Y2S3 Blood Orchid", "v11432634" }) { ToolTipText = "Y2S3 Blood Orchid - v11432634" },
            new ListViewItem(new string[] { "Y2S3 Blood Orchid", "v11493221" }) { ToolTipText = "Y2S3 Blood Orchid - v11493221" },
            new ListViewItem(new string[] { "Y2S4 White Noise", "v11553121" }) { ToolTipText = "Y2S4 White Noise - v11553121" },
            new ListViewItem(new string[] { "Y2S4 White Noise", "v11580709" }) { ToolTipText = "Y2S4 White Noise - v11580709" }
        });
        _columnSeasonLeft.Width = 120;
        _columnBuildLeft.Width = 66;
        _listViewRight.BorderStyle = BorderStyle.None;
        _listViewRight.Columns.AddRange(new ColumnHeader[] { _columnSeasonRight, _columnBuildRight });
        _listViewRight.FullRowSelect = true;
        _listViewRight.GridLines = true;
        _listViewRight.HeaderStyle = ColumnHeaderStyle.None;
        _listViewRight.Location = new Point(205, 19);
        _listViewRight.MultiSelect = false;
        _listViewRight.Name = "listViewRight";
        _listViewRight.ShowItemToolTips = true;
        _listViewRight.Size = new Size(356, 266);
        _listViewRight.TabIndex = 2;
        _listViewRight.UseCompatibleStateImageBehavior = false;
        _listViewRight.View = View.Details;
        _listViewRight.Items.AddRange(new ListViewItem[] {
            new ListViewItem(new string[] { "Y3S1 Chimera (Outbreak)", "v11726982" }) { ToolTipText = "Y3S1 Chimera (Outbreak) - v11726982" },
            new ListViewItem(new string[] { "Y3S2 Para Bellum", "v11938214" }) { ToolTipText = "Y3S2 Para Bellum - v11938214" },
            new ListViewItem(new string[] { "Y3S2 Para Bellum", "v11965022" }) { ToolTipText = "Y3S2 Para Bellum - v11965022" },
            new ListViewItem(new string[] { "Y3S3 Grim Sky", "v12213419" }) { ToolTipText = "Y3S3 Grim Sky - v12213419" },
            new ListViewItem(new string[] { "Y3S3 Grim Sky (Mad House)", "v12362767" }) { ToolTipText = "Y3S3 Grim Sky (Mad House) - v12362767" },
            new ListViewItem(new string[] { "Y3S4 Wind Bastion", "v12512571" }) { ToolTipText = "Y3S4 Wind Bastion - v12512571" },
            new ListViewItem(new string[] { "Y4S1 Burnt Horizon (Rainbow is Magic)", "v12815133" }) { ToolTipText = "Y4S1 Burnt Horizon (Rainbow is Magic) - v12815133" },
            new ListViewItem(new string[] { "Y4S1 Burnt Horizon (Rainbow is Magic)", "v12863847" }) { ToolTipText = "Y4S1 Burnt Horizon (Rainbow is Magic) - v12863847" },
            new ListViewItem(new string[] { "Y4S2 Phantom Sight (Showdown)", "v13147883" }) { ToolTipText = "Y4S2 Phantom Sight (Showdown) - v13147883" },
            new ListViewItem(new string[] { "Y4S3 Ember Rise (Doktors Curse/Money Heist)", "v13632147" }) { ToolTipText = "Y4S3 Ember Rise (Doktors Curse/Money Heist) - v13632147" },
            new ListViewItem(new string[] { "Y4S4 Shifting Tides (Road To S.I. 2020)", "v13777760" }) { ToolTipText = "Y4S4 Shifting Tides (Road To S.I. 2020) - v13777760" },
            new ListViewItem(new string[] { "Y4S4 Shifting Tides (Road To S.I. 2020)", "v13924517" }) { ToolTipText = "Y4S4 Shifting Tides (Road To S.I. 2020) - v13924517" }
        });
        _columnSeasonRight.Width = 278;
        _columnBuildRight.Width = 74;
        _toolTip1.AutomaticDelay = 0;
        _toolTip1.AutoPopDelay = 9999999;
        _toolTip1.InitialDelay = 500;
        _toolTip1.ReshowDelay = 100;
        _toolTip1.ToolTipIcon = ToolTipIcon.Info;
        _toolTip1.ToolTipTitle = "Help";
        _statusTimer.Enabled = true;
        _statusTimer.Interval = 1000;
        _statusTimer.Tick += new EventHandler(OnStatusTick);
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(615, 545);
        this.Controls.Add(_mainTabs);
        this.Controls.Add(_statusStrip1);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.Text = "Liberator";
        _statusStrip1.ResumeLayout(false);
        _statusStrip1.PerformLayout();
        _mainTabs.ResumeLayout(false);
        _tabGametypes.ResumeLayout(false);
        _tabGametypes.PerformLayout();
        _tabMods.ResumeLayout(false);
        _tabMods.PerformLayout();
        _groupBox3.ResumeLayout(false);
        _groupBox3.PerformLayout();
        _groupBox2.ResumeLayout(false);
        _groupBox2.PerformLayout();
        _groupBoxPlayers.ResumeLayout(false);
        _groupBoxPlayers.PerformLayout();
        _groupBoxMap.ResumeLayout(false);
        _groupBoxMap.PerformLayout();
        _groupBoxDisplay.ResumeLayout(false);
        _groupBoxDisplay.PerformLayout();
        _tabAbout.ResumeLayout(false);
        _tabAbout.PerformLayout();
        _groupBox1.ResumeLayout(false);
        _groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private R6S _r6s = new R6S();

    private TreeView _gametypeSource;

    private int _r6sPid;

    private GameBuild _gameBuild = GameBuild.None;

    private bool _buildLabelHintPending = true;

    private bool _pendingAttach = true;

    private bool _modsInitialized;

    private int _statusHoldTicks = 5;

    private long _selectedGametypeId;
    private int _countdown;
    private bool _secondaryWeaponHintPending = true;

    private IContainer _components;

    private StatusStrip _statusStrip1;

    private ToolStripStatusLabel _toolStripStatusLabel1;

    private Timer _attachTimer;

    private TreeView _treeView1;

    private Label _label1;

    private TabControl _mainTabs;

    private TabPage _tabGametypes;

    private TabPage _tabMods;

    private TabPage _tabAbout;

    private GroupBox _groupBox1;

    private LinkLabel _label6;

    private ToolTip _toolTip1;

    private CheckBox _checkBoxOldHereford;

    private CheckBox _checkBoxDisplayVer;

    private CheckBox _checkBoxBottomless;

    private CheckBox _checkBoxHarvard;

    private CheckBox _checkBoxGodMode;

    private CheckBox _checkBoxUnlimitedEquip;

    private Label _labelMadHouse;

    private ComboBox _comboMadHouse;

    private CheckBox _checkBoxDisablePrimaryWeapon;

    private CheckBox _checkBoxDisableGadget;

    private CheckBox _checkBoxSpecial;

    private CheckBox _checkBoxDisableSecondaryWeapon;

    private GroupBox _groupBox2;

    private GroupBox _groupBox3;

    private GroupBox _groupBoxPlayers;

    private GroupBox _groupBoxMap;

    private GroupBox _groupBoxDisplay;

    private CheckBox _checkBoxInfTime;

    private ListView _listViewLeft;

    private ListView _listViewRight;

    private ColumnHeader _columnSeasonLeft;

    private ColumnHeader _columnBuildLeft;

    private ColumnHeader _columnSeasonRight;

    private ColumnHeader _columnBuildRight;

    private CheckBox _checkBoxDisableAI;

    private Button _button2;

    private Button _button1;

    private Timer _statusTimer;
}
