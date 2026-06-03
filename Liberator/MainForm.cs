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
        SetupOperatorButtons();
        _tabOperators.Dispose();
        this.Text = "Rainbow Six Siege Liberator - " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        LoadRegistry();
    }

    private void SetupOperatorButtons()
    {
        _operatorButtonsTeam1[0] = _opPlayer0;
        _operatorButtonsTeam1[1] = _opPlayer1;
        _operatorButtonsTeam1[2] = _opPlayer2;
        _operatorButtonsTeam1[3] = _opPlayer3;
        _operatorButtonsTeam1[4] = _opPlayer4;
        _operatorButtonsTeam2[0] = _opPlayer5;
        _operatorButtonsTeam2[1] = _opPlayer6;
        _operatorButtonsTeam2[2] = _opPlayer7;
        _operatorButtonsTeam2[3] = _opPlayer8;
        _operatorButtonsTeam2[4] = _opPlayer9;
        _comboMadHouse.SelectedIndex = 0;
    }

    private void OnStatusStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
    }

    private void OnLoad(object sender, EventArgs e)
    {
    }

    private void SaveRegistry()
    {
        RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Cheato\\R6Liberator");
        registryKey.SetValue("LastSiegePID", _siegePid.ToString());
        registryKey.SetValue("LiberatorVersion", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        registryKey.SetValue("HasSeenDiscord", _hasSeenDiscord);
    }

    private void LoadRegistry()
    {
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Cheato\\R6Liberator");
        if (registryKey != null)
        {
            _siegePid = Convert.ToInt32(registryKey.GetValue("LastSiegePID"));
            string text = "";
            string text2 = "";
            try
            {
                text = registryKey.GetValue("LiberatorVersion").ToString();
                text2 = registryKey.GetValue("HasSeenDiscord").ToString();
            }
            catch
            {
                SaveRegistry();
            }
            if (text != Assembly.GetExecutingAssembly().GetName().Version.ToString() || text2 != "True")
            {
                if (MessageBox.Show("Thanks for downloading Liberator. Would you like to join the Discord?\nUpdates for Liberator and other Rainbow Six Siege modding will take place there.", "Hey ;)", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Process.Start("https://discord.com/invite/r6s-operation-throwback-2-0-1092820800203141130");
                }
                _hasSeenDiscord = true;
                SaveRegistry();
            }
            _hasSeenDiscord = true;
        }
    }

    private void OnAttachTick(object sender, EventArgs e)
    {
        _siege.Reattach();
        if (_siege.IsAttached())
        {
            _gameBuild = _siege.DetectBuild();
            if (_gameBuild == GameBuild.None)
            {
                ClearGametypes();
                _pendingAttach = true;
                SetStatus("Build not supported :(", false);
                _modsInitialized = false;
                return;
            }
            if (!_pendingAttach)
            {
                if (_countdown == 0)
                {
                    if (!_modsInitialized)
                    {
                        _siegePid = _siege.KillGuardThread(_siegePid);
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
                if (!_siege.IsFullyLaunched())
                {
                    SetStatus("Waiting for Siege to fully launch...", false);
                    _countdown = 5;
                    return;
                }
                _gameBuild = _siege.DetectBuild();
                SetStatus("Found: " + Siege.SeasonNames[(int)_siege.GetSeason()] + " v" + _siege.GetBuildVersion(_gameBuild), false);
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
            _siege.Reattach();
            ClearGametypes();
            _pendingAttach = true;
            SetStatus("Can't find Siege. Make sure BattlEye is disabled and the game is at the main menu!", false);
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
        if (_siege.GetSeason() < Season.Y2S1_Velvet_Shell)
        {
            _checkBoxHarvard.Enabled = true;
            _siege.SetHarvard(_checkBoxHarvard.Checked);
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
            _siege.SetOldHereford(_checkBoxOldHereford.Checked);
        }
        if (_gameBuild == GameBuild.Y3S3_12362767)
        {
            _comboMadHouse.Visible = true;
            _labelMadHouse.Visible = true;
            _siege.SetMadHouseMode(_comboMadHouse.SelectedIndex);
        }
        else
        {
            _labelMadHouse.Visible = false;
            _comboMadHouse.Visible = false;
        }
        if (_siege.GetSeason() >= Season.Y2S4_White_Noise)
        {
            _checkBoxDisplayVer.Enabled = true;
            _siege.SetDisplayVersion(_checkBoxDisplayVer.Checked);
            if (_checkBoxDisplayVer.Checked && _buildLabelHintPending)
            {
                _buildLabelHintPending = false;
                MessageBox.Show("You will need to change your video resolution for the build label to display.", "Build Display");
            }
        }
        else
        {
            _checkBoxDisplayVer.Enabled = false;
        }
        _siege.ApplyCorePatch(true);
        if (_checkBoxDisablePrimaryWeapon.Checked && _checkBoxDisableSecondaryWeapon.Checked && _siege.GetSeason() < Season.Y2S3_Blood_Orchid)
        {
            _siege.SetEmptySecondary(true);
            _siege.SetDisablePrimaryWeapon(_checkBoxDisablePrimaryWeapon.Checked);
            if (_secondaryWeaponHintPending)
            {
                _secondaryWeaponHintPending = false;
                MessageBox.Show("Versions before Blood Orchid won't allow players to move without at least one weapon, so we will give you a secondary weapon with no ammo :)");
            }
        }
        else
        {
            _siege.SetEmptySecondary(false);
            _siege.SetDisablePrimaryWeapon(_checkBoxDisablePrimaryWeapon.Checked);
            _siege.SetDisableSecondaryWeapon(_checkBoxDisableSecondaryWeapon.Checked);
        }
        _siege.SetSpecialAbility(_checkBoxSpecial.Checked);
        _siege.SetDisableGadget(_checkBoxDisableGadget.Checked);
        _siege.SetUnlimitedEquipment(_checkBoxUnlimitedEquip.Checked);
        _siege.SetGodMode(_checkBoxGodMode.Checked);
        _siege.SetDisableAI(_checkBoxDisableAI.Checked);
        _siege.SetBottomlessMagazine(_checkBoxBottomless.Checked);
        if ((string)_checkBoxInfTime.Tag != "1")
        {
            _checkBoxInfTime.Checked = _siege.GetInfiniteTime();
        }
        else
        {
            _siege.SetInfiniteTime(_checkBoxInfTime.Checked);
            _checkBoxInfTime.Tag = "0";
        }
        if (_selectedGametypeId != 0L)
        {
            _siege.SetGametype(_selectedGametypeId);
        }
        if (_siege.GetSeason() < Season.Y1S2_Dust_Line)
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
        _gametypeSource = _siege.BuildGametypeTree();
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
            if (_treeView1.SelectedNode.Nodes.Count == 0 && _siege.IsAttached())
            {
                _siege.SetGametype(Convert.ToInt64(_treeView1.SelectedNode.Name));
                _selectedGametypeId = Convert.ToInt64(_treeView1.SelectedNode.Name);
                if ((string)_treeView1.Tag != _treeView1.SelectedNode.Name)
                {
                    SetStatus("Gametype Set to: " + _treeView1.SelectedNode.FullPath, false);
                    _treeView1.Tag = _treeView1.SelectedNode.Name;
                }
            }
        }
        catch
        {
        }
    }


    private void OnDiscordLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start("https://discord.com/invite/r6s-operation-throwback-2-0-1092820800203141130");
    }

    private void OnOperatorButtonClick(object sender, EventArgs e)
    {
        if ((sender as Button).BackColor == Color.LightBlue)
        {
            (sender as Button).BackColor = Color.Transparent;
        }
        else
        {
            (sender as Button).BackColor = Color.LightBlue;
        }
        Button[] array = new Button[10];
        Array.Copy(_operatorButtonsTeam1, array, 5);
        Array.ConstrainedCopy(_operatorButtonsTeam2, 0, array, 5, 5);
        Button[] array2 = array;
        for (int i = 0; i < array2.Length; i++)
        {
            if (array2[i].BackColor == Color.LightBlue)
            {
                _opChange.Enabled = true;
                return;
            }
        }
        _opChange.Enabled = false;
    }

    private void OnOperatorsTabClick(object sender, EventArgs e)
    {
    }

    private void OnChangeOperatorClick(object sender, EventArgs e)
    {
    }

    private void OnSelectAllOperatorsChanged(object sender, EventArgs e)
    {
    }

    private void OnInfiniteTimeToggled(object sender, EventArgs e)
    {
        (sender as CheckBox).Tag = "1";
    }

    private void OnButton1Click(object sender, EventArgs e)
    {
        _siege.EndRound();
    }

    private void OnButton2Click(object sender, EventArgs e)
    {
        _siege.EndMatch();
    }

    private void OnStatusTick(object sender, EventArgs e)
    {
        if (_statusHoldTicks > 0)
        {
            _statusHoldTicks--;
        }
        if (_statusHoldTicks == 0)
        {
            SetStatus("Idle...", true);
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
        _mainTabs = new TabControl();
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
        _tabOperators = new TabPage();
        _label4 = new Label();
        _label3 = new Label();
        _opChange = new Button();
        _checkBox1 = new CheckBox();
        _opPlayer5 = new Button();
        _pictureBox2 = new PictureBox();
        _opPlayer9 = new Button();
        _opPlayer8 = new Button();
        _opPlayer7 = new Button();
        _opPlayer6 = new Button();
        _opPlayer0 = new Button();
        _pictureBox1 = new PictureBox();
        _opPlayer4 = new Button();
        _opPlayer3 = new Button();
        _opPlayer2 = new Button();
        _opPlayer1 = new Button();
        _tabAbout = new TabPage();
        _linkLabel1 = new LinkLabel();
        _label6 = new Label();
        _groupBox1 = new GroupBox();
        _textBox1 = new TextBox();
        _toolTip1 = new ToolTip(_components);
        _statusTimer = new Timer(_components);
        _statusStrip1.SuspendLayout();
        _mainTabs.SuspendLayout();
        _tabGametypes.SuspendLayout();
        _tabMods.SuspendLayout();
        _groupBox3.SuspendLayout();
        _groupBox2.SuspendLayout();
        _tabOperators.SuspendLayout();
        ((ISupportInitialize)(_pictureBox2)).BeginInit();
        ((ISupportInitialize)(_pictureBox1)).BeginInit();
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
        _statusStrip1.ItemClicked += new ToolStripItemClickedEventHandler(OnStatusStripItemClicked);
        _toolStripStatusLabel1.Name = "toolStripStatusLabel1";
        _toolStripStatusLabel1.Size = new Size(600, 17);
        _toolStripStatusLabel1.Spring = true;
        _toolStripStatusLabel1.Text = "Sup";
        _attachTimer.Enabled = true;
        _attachTimer.Interval = 1000;
        _attachTimer.Tick += new EventHandler(OnAttachTick);
        _treeView1.Location = new Point(9, 32);
        _treeView1.Name = "treeView1";
        _treeView1.Size = new Size(577, 409);
        _treeView1.TabIndex = 9;
        _toolTip1.SetToolTip(_treeView1, "Double click the desired gametype to activate it. Make sure you are in a custom game lobby!");
        _treeView1.Click += new EventHandler(OnGametypeSelected);
        _label1.AutoSize = true;
        _label1.Location = new Point(113, 3);
        _label1.Name = "label1";
        _label1.Size = new Size(330, 26);
        _label1.TabIndex = 12;
        _label1.Text = "Start a Custom Game then double click the desired game mode. \r\nPVE will crash on any build after 1.0 if anyone is on the orange team.";
        _mainTabs.Controls.Add(_tabGametypes);
        _mainTabs.Controls.Add(_tabMods);
        _mainTabs.Controls.Add(_tabOperators);
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
        _comboMadHouse.Location = new Point(269, 447);
        _comboMadHouse.Name = "comboMadHouse";
        _comboMadHouse.Size = new Size(121, 21);
        _comboMadHouse.TabIndex = 19;
        _comboMadHouse.Visible = false;
        _labelMadHouse.AutoSize = true;
        _labelMadHouse.Location = new Point(150, 450);
        _labelMadHouse.Name = "labelMadHouse";
        _labelMadHouse.Size = new Size(113, 13);
        _labelMadHouse.TabIndex = 18;
        _labelMadHouse.Text = "Mad House Gametype";
        _labelMadHouse.Visible = false;
        _tabMods.Controls.Add(_groupBox3);
        _tabMods.Controls.Add(_groupBox2);
        _tabMods.Controls.Add(_checkBoxOldHereford);
        _tabMods.Controls.Add(_checkBoxDisplayVer);
        _tabMods.Controls.Add(_checkBoxBottomless);
        _tabMods.Controls.Add(_checkBoxHarvard);
        _tabMods.Controls.Add(_checkBoxGodMode);
        _tabMods.Controls.Add(_checkBoxUnlimitedEquip);
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
        _groupBox3.Location = new Point(10, 273);
        _groupBox3.Name = "groupBox3";
        _groupBox3.Size = new Size(248, 93);
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
        _checkBoxDisableAI.Text = "Brain Dead AI (set before match start)";
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
        _groupBox2.Location = new Point(10, 144);
        _groupBox2.Name = "groupBox2";
        _groupBox2.Size = new Size(248, 123);
        _groupBox2.TabIndex = 27;
        _groupBox2.TabStop = false;
        _groupBox2.Text = "Loadout (Set before spawn)";
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
        _checkBoxOldHereford.AutoSize = true;
        _checkBoxOldHereford.Location = new Point(10, 6);
        _checkBoxOldHereford.Name = "checkBoxOldHereford";
        _checkBoxOldHereford.Size = new Size(141, 17);
        _checkBoxOldHereford.TabIndex = 22;
        _checkBoxOldHereford.Text = "Enable Original Hereford";
        _toolTip1.SetToolTip(_checkBoxOldHereford, "Replaces the Hereford rework with the original version.\r\nHost authority.");
        _checkBoxOldHereford.UseVisualStyleBackColor = true;
        _checkBoxDisplayVer.AutoSize = true;
        _checkBoxDisplayVer.Location = new Point(10, 121);
        _checkBoxDisplayVer.Name = "checkBoxDisplayVer";
        _checkBoxDisplayVer.Size = new Size(124, 17);
        _checkBoxDisplayVer.TabIndex = 21;
        _checkBoxDisplayVer.Text = "Display Build Version";
        _toolTip1.SetToolTip(_checkBoxDisplayVer, componentResourceManager.GetString("checkBoxDisplayVer.ToolTip"));
        _checkBoxDisplayVer.UseVisualStyleBackColor = true;
        _checkBoxBottomless.AutoSize = true;
        _checkBoxBottomless.Location = new Point(10, 98);
        _checkBoxBottomless.Name = "checkBoxBottomless";
        _checkBoxBottomless.Size = new Size(164, 17);
        _checkBoxBottomless.TabIndex = 20;
        _checkBoxBottomless.Text = "Bottomless Clip (Host, Buggy)";
        _toolTip1.SetToolTip(_checkBoxBottomless, "Gives all players a ton of ammo so they never have to reload. \r\nYou must enable this before spawning.\r\nHost authority.");
        _checkBoxBottomless.UseVisualStyleBackColor = true;
        _checkBoxHarvard.AutoSize = true;
        _checkBoxHarvard.Location = new Point(10, 29);
        _checkBoxHarvard.Name = "checkBoxHarvard";
        _checkBoxHarvard.Size = new Size(238, 17);
        _checkBoxHarvard.TabIndex = 17;
        _checkBoxHarvard.Text = "Replace House with Bartlett University (Host)";
        _toolTip1.SetToolTip(_checkBoxHarvard, "Create a playlist with House and when you start the match it will load up Bartlett University instead.\r\nHost authority.");
        _checkBoxHarvard.UseVisualStyleBackColor = true;
        _checkBoxGodMode.AutoSize = true;
        _checkBoxGodMode.Location = new Point(10, 75);
        _checkBoxGodMode.Name = "checkBoxGodMode";
        _checkBoxGodMode.Size = new Size(205, 17);
        _checkBoxGodMode.TabIndex = 19;
        _checkBoxGodMode.Text = "Deathless Players and Hostage (Host)";
        _toolTip1.SetToolTip(_checkBoxGodMode, "Prevents all players and the hostage from dying from most forms of damage.\r\nHost authority.");
        _checkBoxGodMode.UseVisualStyleBackColor = true;
        _checkBoxUnlimitedEquip.AutoSize = true;
        _checkBoxUnlimitedEquip.Location = new Point(10, 52);
        _checkBoxUnlimitedEquip.Name = "checkBoxUnlimitedEquip";
        _checkBoxUnlimitedEquip.Size = new Size(153, 17);
        _checkBoxUnlimitedEquip.TabIndex = 18;
        _checkBoxUnlimitedEquip.Text = "Unlimited Equipment (Host)";
        _toolTip1.SetToolTip(_checkBoxUnlimitedEquip, "All players will have unlimited equipment and reinforcements.\r\nHost authority.");
        _checkBoxUnlimitedEquip.UseVisualStyleBackColor = true;
        _tabOperators.Controls.Add(_label4);
        _tabOperators.Controls.Add(_label3);
        _tabOperators.Controls.Add(_opChange);
        _tabOperators.Controls.Add(_checkBox1);
        _tabOperators.Controls.Add(_opPlayer5);
        _tabOperators.Controls.Add(_pictureBox2);
        _tabOperators.Controls.Add(_opPlayer9);
        _tabOperators.Controls.Add(_opPlayer8);
        _tabOperators.Controls.Add(_opPlayer7);
        _tabOperators.Controls.Add(_opPlayer6);
        _tabOperators.Controls.Add(_opPlayer0);
        _tabOperators.Controls.Add(_pictureBox1);
        _tabOperators.Controls.Add(_opPlayer4);
        _tabOperators.Controls.Add(_opPlayer3);
        _tabOperators.Controls.Add(_opPlayer2);
        _tabOperators.Controls.Add(_opPlayer1);
        _tabOperators.Location = new Point(4, 22);
        _tabOperators.Name = "tabOperators";
        _tabOperators.Size = new Size(592, 482);
        _tabOperators.TabIndex = 2;
        _tabOperators.Text = "Operators";
        _tabOperators.UseVisualStyleBackColor = true;
        _tabOperators.Click += new EventHandler(OnOperatorsTabClick);
        _label4.AutoSize = true;
        _label4.Font = new Font("Stencil", (float)(20.25f), FontStyle.Bold, GraphicsUnit.Point, 0);
        _label4.ForeColor = Color.FromArgb(212, 103, 48);
        _label4.Location = new Point(199, 212);
        _label4.Name = "label4";
        _label4.Size = new Size(207, 32);
        _label4.TabIndex = 61;
        _label4.Text = "Orange Team";
        _label3.AutoSize = true;
        _label3.Font = new Font("Stencil", (float)(20.25f), FontStyle.Bold, GraphicsUnit.Point, 0);
        _label3.ForeColor = Color.FromArgb(49, 127, 215);
        _label3.Location = new Point(199, 14);
        _label3.Name = "label3";
        _label3.Size = new Size(167, 32);
        _label3.TabIndex = 54;
        _label3.Text = "Blue Team";
        _opChange.Enabled = false;
        _opChange.Font = new Font("Microsoft Sans Serif", (float)(14f), FontStyle.Regular, GraphicsUnit.Point, 0);
        _opChange.Location = new Point(239, 417);
        _opChange.Name = "opChange";
        _opChange.Size = new Size(112, 57);
        _opChange.TabIndex = 46;
        _opChange.Text = "Change Operator";
        _toolTip1.SetToolTip(_opChange, "Select one or more players to override their operators.");
        _opChange.UseVisualStyleBackColor = true;
        _opChange.Click += new EventHandler(OnChangeOperatorClick);
        _checkBox1.AutoSize = true;
        _checkBox1.Location = new Point(3, 3);
        _checkBox1.Name = "checkBox1";
        _checkBox1.Size = new Size(59, 17);
        _checkBox1.TabIndex = 26;
        _checkBox1.Tag = "0";
        _checkBox1.Text = "Enable";
        _checkBox1.UseVisualStyleBackColor = true;
        _checkBox1.CheckedChanged += new EventHandler(OnSelectAllOperatorsChanged);
        _opPlayer5.Image = (Image)componentResourceManager.GetObject("opPlayer5.Image");
        _opPlayer5.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer5.Location = new Point(3, 264);
        _opPlayer5.Name = "opPlayer5";
        _opPlayer5.Size = new Size(112, 128);
        _opPlayer5.TabIndex = 55;
        _opPlayer5.Text = "Player 0";
        _opPlayer5.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer5.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer5.UseVisualStyleBackColor = true;
        _opPlayer5.Click += new EventHandler(OnOperatorButtonClick);
        _pictureBox2.Image = OperatorImages.Defenders;
        _pictureBox2.Location = new Point(133, 201);
        _pictureBox2.Name = "pictureBox2";
        _pictureBox2.Size = new Size(60, 56);
        _pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        _pictureBox2.TabIndex = 60;
        _pictureBox2.TabStop = false;
        _opPlayer9.Image = (Image)componentResourceManager.GetObject("opPlayer9.Image");
        _opPlayer9.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer9.Location = new Point(476, 264);
        _opPlayer9.Name = "opPlayer9";
        _opPlayer9.Size = new Size(112, 128);
        _opPlayer9.TabIndex = 59;
        _opPlayer9.Text = "Player 4";
        _opPlayer9.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer9.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer9.UseVisualStyleBackColor = true;
        _opPlayer9.Click += new EventHandler(OnOperatorButtonClick);
        _opPlayer8.Image = (Image)componentResourceManager.GetObject("opPlayer8.Image");
        _opPlayer8.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer8.Location = new Point(357, 264);
        _opPlayer8.Name = "opPlayer8";
        _opPlayer8.Size = new Size(113, 128);
        _opPlayer8.TabIndex = 58;
        _opPlayer8.Text = "Player 3";
        _opPlayer8.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer8.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer8.UseVisualStyleBackColor = true;
        _opPlayer8.Click += new EventHandler(OnOperatorButtonClick);
        _opPlayer7.Image = (Image)componentResourceManager.GetObject("opPlayer7.Image");
        _opPlayer7.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer7.Location = new Point(239, 264);
        _opPlayer7.Name = "opPlayer7";
        _opPlayer7.Size = new Size(112, 128);
        _opPlayer7.TabIndex = 57;
        _opPlayer7.Text = "Player 2";
        _opPlayer7.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer7.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer7.UseVisualStyleBackColor = true;
        _opPlayer7.Click += new EventHandler(OnOperatorButtonClick);
        _opPlayer6.Image = (Image)componentResourceManager.GetObject("opPlayer6.Image");
        _opPlayer6.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer6.Location = new Point(121, 264);
        _opPlayer6.Name = "opPlayer6";
        _opPlayer6.Size = new Size(112, 128);
        _opPlayer6.TabIndex = 56;
        _opPlayer6.Text = "Player 1";
        _opPlayer6.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer6.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer6.UseVisualStyleBackColor = true;
        _opPlayer6.Click += new EventHandler(OnOperatorButtonClick);
        _opPlayer0.Image = OperatorImages.NoOperator;
        _opPlayer0.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer0.Location = new Point(3, 66);
        _opPlayer0.Name = "opPlayer0";
        _opPlayer0.Size = new Size(112, 128);
        _opPlayer0.TabIndex = 48;
        _opPlayer0.Text = "Player 0";
        _opPlayer0.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer0.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer0.UseVisualStyleBackColor = true;
        _opPlayer0.Click += new EventHandler(OnOperatorButtonClick);
        _pictureBox1.Image = OperatorImages.Attackers;
        _pictureBox1.Location = new Point(133, 3);
        _pictureBox1.Name = "pictureBox1";
        _pictureBox1.Size = new Size(60, 56);
        _pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        _pictureBox1.TabIndex = 53;
        _pictureBox1.TabStop = false;
        _opPlayer4.Image = (Image)componentResourceManager.GetObject("opPlayer4.Image");
        _opPlayer4.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer4.Location = new Point(476, 66);
        _opPlayer4.Name = "opPlayer4";
        _opPlayer4.Size = new Size(112, 128);
        _opPlayer4.TabIndex = 52;
        _opPlayer4.Text = "Player 4";
        _opPlayer4.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer4.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer4.UseVisualStyleBackColor = true;
        _opPlayer4.Click += new EventHandler(OnOperatorButtonClick);
        _opPlayer3.Image = (Image)componentResourceManager.GetObject("opPlayer3.Image");
        _opPlayer3.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer3.Location = new Point(357, 66);
        _opPlayer3.Name = "opPlayer3";
        _opPlayer3.Size = new Size(113, 128);
        _opPlayer3.TabIndex = 51;
        _opPlayer3.Text = "Player 3";
        _opPlayer3.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer3.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer3.UseVisualStyleBackColor = true;
        _opPlayer3.Click += new EventHandler(OnOperatorButtonClick);
        _opPlayer2.Image = (Image)componentResourceManager.GetObject("opPlayer2.Image");
        _opPlayer2.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer2.Location = new Point(239, 66);
        _opPlayer2.Name = "opPlayer2";
        _opPlayer2.Size = new Size(112, 128);
        _opPlayer2.TabIndex = 50;
        _opPlayer2.Text = "Player 2";
        _opPlayer2.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer2.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer2.UseVisualStyleBackColor = true;
        _opPlayer2.Click += new EventHandler(OnOperatorButtonClick);
        _opPlayer1.Image = (Image)componentResourceManager.GetObject("opPlayer1.Image");
        _opPlayer1.ImageAlign = ContentAlignment.TopCenter;
        _opPlayer1.Location = new Point(121, 66);
        _opPlayer1.Name = "opPlayer1";
        _opPlayer1.Size = new Size(112, 128);
        _opPlayer1.TabIndex = 49;
        _opPlayer1.Text = "Player 1";
        _opPlayer1.TextAlign = ContentAlignment.BottomCenter;
        _opPlayer1.TextImageRelation = TextImageRelation.ImageAboveText;
        _opPlayer1.UseVisualStyleBackColor = true;
        _opPlayer1.Click += new EventHandler(OnOperatorButtonClick);
        _tabAbout.Controls.Add(_linkLabel1);
        _tabAbout.Controls.Add(_label6);
        _tabAbout.Controls.Add(_groupBox1);
        _tabAbout.Location = new Point(4, 22);
        _tabAbout.Name = "tabAbout";
        _tabAbout.Size = new Size(592, 482);
        _tabAbout.TabIndex = 3;
        _tabAbout.Text = "About";
        _tabAbout.UseVisualStyleBackColor = true;
        _linkLabel1.AutoSize = true;
        _linkLabel1.Location = new Point(3, 148);
        _linkLabel1.Name = "linkLabel1";
        _linkLabel1.Size = new Size(259, 13);
        _linkLabel1.TabIndex = 2;
        _linkLabel1.TabStop = true;
        _linkLabel1.Text = "R6S: Operation Throwback 2.0";
        _linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(OnDiscordLinkClicked);
        _label6.AutoSize = false;
        _label6.Font = new Font("Microsoft Sans Serif", (float)(8.25f), FontStyle.Regular, GraphicsUnit.Point, 0);
        _label6.Location = new Point(3, 9);
        _label6.Name = "label6";
        _label6.Size = new Size(488, 130);
        _label6.TabIndex = 1;
        _label6.Text = string.Join("\r\n", new string[] { "This tool is for expanding the possibilities of Tom Clancy's Rainbow Six\u00AE Siege in local custom games. You can not use this to cheat in matchmaking and will never support that.", "", "Made by Gamecheat13 (Cheato)", "Special thanks to the research by Akosai, RolyNoly, and Smartie. Some of this wouldnt be possible without the ground work these fellas did.", "Thanks to Big Chungus for his unlock all research :)", "Shout out to SexyMasterChief" });
        _groupBox1.Controls.Add(_textBox1);
        _groupBox1.Location = new Point(3, 188);
        _groupBox1.Name = "groupBox1";
        _groupBox1.Size = new Size(584, 291);
        _groupBox1.TabIndex = 0;
        _groupBox1.TabStop = false;
        _groupBox1.Text = "Supported Versions";
        _textBox1.Location = new Point(6, 19);
        _textBox1.Multiline = true;
        _textBox1.Name = "textBox1";
        _textBox1.ReadOnly = true;
        _textBox1.ScrollBars = ScrollBars.Vertical;
        _textBox1.Size = new Size(572, 266);
        _textBox1.TabIndex = 1;
        _textBox1.Text = string.Join("\r\n", new string[] { "Y1S0 Vanilla - v8194013", "Y1S1 Black Ice - v8519860", "Y1S2 Dust Line - v9132097", "Y1S3 Skull Rain - v9654076", "Y1S3 Skull Rain - v9860556", "Y1S4 Red Crow - v10211195", "Y2S1 Velvet Shell - v10751226", "Y2S2 Health - v11216230", "Y2S3 Blood Orchid - v11432634", "Y2S3 Blood Orchid - v11493221", "Y2S4 White Noise - v11553121", "Y2S4 White Noise - v11580709", "Y3S1 Chimera (Outbreak) - v11726982", "Y3S2 Para Bellum - v11938214", "Y3S2 Para Bellum - v11965022", "Y3S3 Grim Sky - v12213419", "Y3S3 Grim Sky (Mad House) - v12362767", "Y3S4 Wind Bastion - v12512571", "Y4S1 Burnt Horizon (Rainbow is Magic) - v12815133", "Y4S1 Burnt Horizon (Rainbow is Magic) - v12863847", "Y4S2 Phantom Sight (Showdown) - v13147883", "Y4S3 Ember Rise (Doktors Curse/Money Heist) - v13632147", "Y4S4 Shifting Tides (Road To S.I. 2020) - v13777760", "Y4S4 Shifting Tides (Road To S.I. 2020) - v13924517" });
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
        this.Text = "Rainbow Six Siege Liberator";
        this.Load += new EventHandler(OnLoad);
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
        _tabOperators.ResumeLayout(false);
        _tabOperators.PerformLayout();
        ((ISupportInitialize)(_pictureBox2)).EndInit();
        ((ISupportInitialize)(_pictureBox1)).EndInit();
        _tabAbout.ResumeLayout(false);
        _tabAbout.PerformLayout();
        _groupBox1.ResumeLayout(false);
        _groupBox1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private Siege _siege = new Siege();

    private TreeView _gametypeSource;

    private int _siegePid;

    private GameBuild _gameBuild = GameBuild.None;

    private bool _buildLabelHintPending = true;

    private bool _pendingAttach = true;

    private bool _modsInitialized;

    private int _statusHoldTicks = 5;

    private Button[] _operatorButtonsTeam1 = new Button[5];
    private Button[] _operatorButtonsTeam2 = new Button[5];

    private long _selectedGametypeId;
    private int _countdown;
    private bool _secondaryWeaponHintPending = true;

    private bool _hasSeenDiscord;

    private IContainer _components;

    private StatusStrip _statusStrip1;

    private ToolStripStatusLabel _toolStripStatusLabel1;

    private Timer _attachTimer;

    private TreeView _treeView1;

    private Label _label1;

    private TabControl _mainTabs;

    private TabPage _tabGametypes;

    private TabPage _tabMods;

    private TabPage _tabOperators;

    private TabPage _tabAbout;

    private GroupBox _groupBox1;

    private Label _label6;

    private LinkLabel _linkLabel1;


    private ToolTip _toolTip1;

    private CheckBox _checkBoxOldHereford;

    private CheckBox _checkBoxDisplayVer;

    private CheckBox _checkBoxBottomless;

    private CheckBox _checkBoxHarvard;

    private CheckBox _checkBoxGodMode;

    private CheckBox _checkBoxUnlimitedEquip;

    private CheckBox _checkBox1;

    private Button _opChange;

    private Button _opPlayer0;

    private PictureBox _pictureBox1;

    private Button _opPlayer4;

    private Button _opPlayer3;

    private Button _opPlayer2;

    private Button _opPlayer1;

    private Label _label3;

    private Label _label4;

    private Button _opPlayer5;

    private PictureBox _pictureBox2;

    private Button _opPlayer9;

    private Button _opPlayer8;

    private Button _opPlayer7;

    private Button _opPlayer6;

    private Label _labelMadHouse;

    private ComboBox _comboMadHouse;

    private CheckBox _checkBoxDisablePrimaryWeapon;

    private CheckBox _checkBoxDisableGadget;

    private CheckBox _checkBoxSpecial;

    private CheckBox _checkBoxDisableSecondaryWeapon;

    private GroupBox _groupBox2;

    private GroupBox _groupBox3;

    private CheckBox _checkBoxInfTime;

    private TextBox _textBox1;

    private CheckBox _checkBoxDisableAI;

    private Button _button2;

    private Button _button1;

    private Timer _statusTimer;
}
