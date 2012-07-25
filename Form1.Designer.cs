namespace Chromophobia
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabChromo = new System.Windows.Forms.TabPage();
            this.buttonChromoBatch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectModelDatabase = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.selectChromosome = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericProbability = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericCuts = new System.Windows.Forms.NumericUpDown();
            this.numericRuns = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabImport = new System.Windows.Forms.TabPage();
            this.buttonConvertChromosome = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonTestNull = new System.Windows.Forms.Button();
            this.textNullmodelName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonCreateNullModel = new System.Windows.Forms.Button();
            this.buttonEnsemblSqlite = new System.Windows.Forms.Button();
            this.buttonImportEnsemblSynteny = new System.Windows.Forms.Button();
            this.tabTest = new System.Windows.Forms.TabPage();
            this.textCREnd = new System.Windows.Forms.TextBox();
            this.textCRStart = new System.Windows.Forms.TextBox();
            this.textCRChromosome = new System.Windows.Forms.TextBox();
            this.textCRRes = new System.Windows.Forms.TextBox();
            this.buttonChromosomeRead = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tabHmmer = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelHmmFileResult = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonHmmCreateFile = new System.Windows.Forms.Button();
            this.textHmmFilename = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textHmmDomains = new System.Windows.Forms.TextBox();
            this.buttonHmmClear = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textHmmTitle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textHmmSequence2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonHmmscan = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textHmmResults = new System.Windows.Forms.TextBox();
            this.textHmmSequence = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.checkExtendedOutput = new System.Windows.Forms.CheckBox();
            this.tabControl.SuspendLayout();
            this.tabChromo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericProbability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCuts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRuns)).BeginInit();
            this.tabImport.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabTest.SuspendLayout();
            this.tabHmmer.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(935, 575);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Url = new System.Uri("", System.UriKind.Relative);
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabChromo);
            this.tabControl.Controls.Add(this.tabImport);
            this.tabControl.Controls.Add(this.tabTest);
            this.tabControl.Controls.Add(this.tabHmmer);
            this.tabControl.Controls.Add(this.tabOutput);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(949, 610);
            this.tabControl.TabIndex = 1;
            // 
            // tabChromo
            // 
            this.tabChromo.Controls.Add(this.buttonChromoBatch);
            this.tabChromo.Controls.Add(this.groupBox1);
            this.tabChromo.Location = new System.Drawing.Point(4, 25);
            this.tabChromo.Name = "tabChromo";
            this.tabChromo.Padding = new System.Windows.Forms.Padding(3);
            this.tabChromo.Size = new System.Drawing.Size(941, 581);
            this.tabChromo.TabIndex = 0;
            this.tabChromo.Text = "Chromothripsis";
            this.tabChromo.UseVisualStyleBackColor = true;
            // 
            // buttonChromoBatch
            // 
            this.buttonChromoBatch.Font = new System.Drawing.Font("Lucida Sans Unicode", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonChromoBatch.Location = new System.Drawing.Point(657, 509);
            this.buttonChromoBatch.Name = "buttonChromoBatch";
            this.buttonChromoBatch.Size = new System.Drawing.Size(194, 34);
            this.buttonChromoBatch.TabIndex = 18;
            this.buttonChromoBatch.Text = "Chromothripsis Batch";
            this.buttonChromoBatch.UseVisualStyleBackColor = true;
            this.buttonChromoBatch.Click += new System.EventHandler(this.buttonChromoBatch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkExtendedOutput);
            this.groupBox1.Controls.Add(this.selectModelDatabase);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.selectChromosome);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericProbability);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericCuts);
            this.groupBox1.Controls.Add(this.numericRuns);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonRun);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(59, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(792, 458);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // selectModelDatabase
            // 
            this.selectModelDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectModelDatabase.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectModelDatabase.FormattingEnabled = true;
            this.selectModelDatabase.Items.AddRange(new object[] {
            "Chromo",
            "Pfam-A"});
            this.selectModelDatabase.Location = new System.Drawing.Point(445, 147);
            this.selectModelDatabase.Name = "selectModelDatabase";
            this.selectModelDatabase.Size = new System.Drawing.Size(130, 28);
            this.selectModelDatabase.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(213, 149);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(127, 20);
            this.label14.TabIndex = 10;
            this.label14.Text = "HMM Database";
            // 
            // selectChromosome
            // 
            this.selectChromosome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectChromosome.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectChromosome.FormattingEnabled = true;
            this.selectChromosome.Location = new System.Drawing.Point(445, 102);
            this.selectChromosome.Name = "selectChromosome";
            this.selectChromosome.Size = new System.Drawing.Size(130, 28);
            this.selectChromosome.TabIndex = 9;
            this.selectChromosome.SelectedIndexChanged += new System.EventHandler(this.selectChromosome_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(213, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Chromosome";
            // 
            // numericProbability
            // 
            this.numericProbability.DecimalPlaces = 2;
            this.numericProbability.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericProbability.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericProbability.Location = new System.Drawing.Point(445, 291);
            this.numericProbability.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericProbability.Name = "numericProbability";
            this.numericProbability.Size = new System.Drawing.Size(130, 32);
            this.numericProbability.TabIndex = 7;
            this.numericProbability.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(213, 293);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Probability of deletion";
            // 
            // numericCuts
            // 
            this.numericCuts.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericCuts.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericCuts.Location = new System.Drawing.Point(445, 243);
            this.numericCuts.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericCuts.Name = "numericCuts";
            this.numericCuts.Size = new System.Drawing.Size(130, 32);
            this.numericCuts.TabIndex = 5;
            this.numericCuts.ThousandsSeparator = true;
            this.numericCuts.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numericRuns
            // 
            this.numericRuns.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericRuns.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericRuns.Location = new System.Drawing.Point(445, 194);
            this.numericRuns.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericRuns.Name = "numericRuns";
            this.numericRuns.Size = new System.Drawing.Size(130, 32);
            this.numericRuns.TabIndex = 4;
            this.numericRuns.ThousandsSeparator = true;
            this.numericRuns.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(213, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Cuts per run";
            // 
            // buttonRun
            // 
            this.buttonRun.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRun.Location = new System.Drawing.Point(268, 381);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(247, 43);
            this.buttonRun.TabIndex = 2;
            this.buttonRun.Text = "Run Simulations";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(213, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of runs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(80, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(659, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chromothripsis Simulation Suite";
            // 
            // tabImport
            // 
            this.tabImport.Controls.Add(this.buttonConvertChromosome);
            this.tabImport.Controls.Add(this.groupBox3);
            this.tabImport.Controls.Add(this.buttonEnsemblSqlite);
            this.tabImport.Controls.Add(this.buttonImportEnsemblSynteny);
            this.tabImport.Location = new System.Drawing.Point(4, 25);
            this.tabImport.Name = "tabImport";
            this.tabImport.Size = new System.Drawing.Size(941, 581);
            this.tabImport.TabIndex = 2;
            this.tabImport.Text = "Data Import";
            this.tabImport.UseVisualStyleBackColor = true;
            // 
            // buttonConvertChromosome
            // 
            this.buttonConvertChromosome.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConvertChromosome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConvertChromosome.Location = new System.Drawing.Point(22, 142);
            this.buttonConvertChromosome.Name = "buttonConvertChromosome";
            this.buttonConvertChromosome.Size = new System.Drawing.Size(352, 33);
            this.buttonConvertChromosome.TabIndex = 5;
            this.buttonConvertChromosome.Text = "Convert Chromosome 4 FASTA to FAC";
            this.buttonConvertChromosome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonConvertChromosome.UseVisualStyleBackColor = true;
            this.buttonConvertChromosome.Click += new System.EventHandler(this.buttonConvertChromosome_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonTestNull);
            this.groupBox3.Controls.Add(this.textNullmodelName);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.buttonCreateNullModel);
            this.groupBox3.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(22, 253);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(494, 204);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Create Null Model From Chromosome 4q";
            // 
            // buttonTestNull
            // 
            this.buttonTestNull.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTestNull.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTestNull.Location = new System.Drawing.Point(80, 137);
            this.buttonTestNull.Name = "buttonTestNull";
            this.buttonTestNull.Size = new System.Drawing.Size(313, 33);
            this.buttonTestNull.TabIndex = 25;
            this.buttonTestNull.Text = "Test Null Model";
            this.buttonTestNull.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTestNull.UseVisualStyleBackColor = true;
            this.buttonTestNull.Click += new System.EventHandler(this.buttonTestNull_Click);
            // 
            // textNullmodelName
            // 
            this.textNullmodelName.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textNullmodelName.Location = new System.Drawing.Point(154, 49);
            this.textNullmodelName.Name = "textNullmodelName";
            this.textNullmodelName.Size = new System.Drawing.Size(239, 32);
            this.textNullmodelName.TabIndex = 1;
            this.textNullmodelName.Text = "4qnull";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(76, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 20);
            this.label15.TabIndex = 24;
            this.label15.Text = "Name:";
            // 
            // buttonCreateNullModel
            // 
            this.buttonCreateNullModel.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateNullModel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCreateNullModel.Location = new System.Drawing.Point(80, 98);
            this.buttonCreateNullModel.Name = "buttonCreateNullModel";
            this.buttonCreateNullModel.Size = new System.Drawing.Size(313, 33);
            this.buttonCreateNullModel.TabIndex = 0;
            this.buttonCreateNullModel.Text = "Create Null Model";
            this.buttonCreateNullModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCreateNullModel.UseVisualStyleBackColor = true;
            this.buttonCreateNullModel.Click += new System.EventHandler(this.buttonCreateNullModel_Click);
            // 
            // buttonEnsemblSqlite
            // 
            this.buttonEnsemblSqlite.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEnsemblSqlite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEnsemblSqlite.Location = new System.Drawing.Point(22, 42);
            this.buttonEnsemblSqlite.Name = "buttonEnsemblSqlite";
            this.buttonEnsemblSqlite.Size = new System.Drawing.Size(352, 33);
            this.buttonEnsemblSqlite.TabIndex = 2;
            this.buttonEnsemblSqlite.Text = "Import Ensembl Genome Annotation";
            this.buttonEnsemblSqlite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEnsemblSqlite.UseVisualStyleBackColor = true;
            this.buttonEnsemblSqlite.Click += new System.EventHandler(this.buttonEnsemblSqlite_Click);
            // 
            // buttonImportEnsemblSynteny
            // 
            this.buttonImportEnsemblSynteny.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImportEnsemblSynteny.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonImportEnsemblSynteny.Location = new System.Drawing.Point(22, 91);
            this.buttonImportEnsemblSynteny.Name = "buttonImportEnsemblSynteny";
            this.buttonImportEnsemblSynteny.Size = new System.Drawing.Size(352, 33);
            this.buttonImportEnsemblSynteny.TabIndex = 1;
            this.buttonImportEnsemblSynteny.Text = "Import Ensembl Synteny";
            this.buttonImportEnsemblSynteny.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonImportEnsemblSynteny.UseVisualStyleBackColor = true;
            this.buttonImportEnsemblSynteny.Click += new System.EventHandler(this.buttonImportEnsemblSynteny_Click);
            // 
            // tabTest
            // 
            this.tabTest.Controls.Add(this.textCREnd);
            this.tabTest.Controls.Add(this.textCRStart);
            this.tabTest.Controls.Add(this.textCRChromosome);
            this.tabTest.Controls.Add(this.textCRRes);
            this.tabTest.Controls.Add(this.buttonChromosomeRead);
            this.tabTest.Controls.Add(this.label18);
            this.tabTest.Controls.Add(this.label17);
            this.tabTest.Controls.Add(this.label16);
            this.tabTest.Location = new System.Drawing.Point(4, 25);
            this.tabTest.Name = "tabTest";
            this.tabTest.Size = new System.Drawing.Size(941, 581);
            this.tabTest.TabIndex = 4;
            this.tabTest.Text = "Test";
            this.tabTest.UseVisualStyleBackColor = true;
            // 
            // textCREnd
            // 
            this.textCREnd.Location = new System.Drawing.Point(160, 146);
            this.textCREnd.Name = "textCREnd";
            this.textCREnd.Size = new System.Drawing.Size(100, 27);
            this.textCREnd.TabIndex = 15;
            // 
            // textCRStart
            // 
            this.textCRStart.Location = new System.Drawing.Point(160, 107);
            this.textCRStart.Name = "textCRStart";
            this.textCRStart.Size = new System.Drawing.Size(100, 27);
            this.textCRStart.TabIndex = 14;
            // 
            // textCRChromosome
            // 
            this.textCRChromosome.Location = new System.Drawing.Point(160, 67);
            this.textCRChromosome.Name = "textCRChromosome";
            this.textCRChromosome.Size = new System.Drawing.Size(100, 27);
            this.textCRChromosome.TabIndex = 13;
            // 
            // textCRRes
            // 
            this.textCRRes.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textCRRes.Location = new System.Drawing.Point(463, 44);
            this.textCRRes.Multiline = true;
            this.textCRRes.Name = "textCRRes";
            this.textCRRes.ReadOnly = true;
            this.textCRRes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textCRRes.Size = new System.Drawing.Size(422, 367);
            this.textCRRes.TabIndex = 12;
            // 
            // buttonChromosomeRead
            // 
            this.buttonChromosomeRead.Location = new System.Drawing.Point(160, 198);
            this.buttonChromosomeRead.Name = "buttonChromosomeRead";
            this.buttonChromosomeRead.Size = new System.Drawing.Size(75, 23);
            this.buttonChromosomeRead.TabIndex = 3;
            this.buttonChromosomeRead.Text = "Read";
            this.buttonChromosomeRead.UseVisualStyleBackColor = true;
            this.buttonChromosomeRead.Click += new System.EventHandler(this.buttonChromosomeRead_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(40, 149);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 16);
            this.label18.TabIndex = 2;
            this.label18.Text = "End";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(40, 107);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 16);
            this.label17.TabIndex = 1;
            this.label17.Text = "Start";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(37, 67);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 16);
            this.label16.TabIndex = 0;
            this.label16.Text = "Chromosome";
            // 
            // tabHmmer
            // 
            this.tabHmmer.Controls.Add(this.groupBox2);
            this.tabHmmer.Controls.Add(this.buttonHmmClear);
            this.tabHmmer.Controls.Add(this.label10);
            this.tabHmmer.Controls.Add(this.textHmmTitle);
            this.tabHmmer.Controls.Add(this.label9);
            this.tabHmmer.Controls.Add(this.textHmmSequence2);
            this.tabHmmer.Controls.Add(this.label8);
            this.tabHmmer.Controls.Add(this.buttonHmmscan);
            this.tabHmmer.Controls.Add(this.label7);
            this.tabHmmer.Controls.Add(this.textHmmResults);
            this.tabHmmer.Controls.Add(this.textHmmSequence);
            this.tabHmmer.Controls.Add(this.label6);
            this.tabHmmer.Location = new System.Drawing.Point(4, 25);
            this.tabHmmer.Name = "tabHmmer";
            this.tabHmmer.Padding = new System.Windows.Forms.Padding(3);
            this.tabHmmer.Size = new System.Drawing.Size(941, 581);
            this.tabHmmer.TabIndex = 3;
            this.tabHmmer.Text = "Hmmer";
            this.tabHmmer.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelHmmFileResult);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.buttonHmmCreateFile);
            this.groupBox2.Controls.Add(this.textHmmFilename);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.textHmmDomains);
            this.groupBox2.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(33, 399);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(837, 149);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Domains to HMM";
            // 
            // labelHmmFileResult
            // 
            this.labelHmmFileResult.AutoSize = true;
            this.labelHmmFileResult.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHmmFileResult.Location = new System.Drawing.Point(685, 102);
            this.labelHmmFileResult.Name = "labelHmmFileResult";
            this.labelHmmFileResult.Size = new System.Drawing.Size(0, 20);
            this.labelHmmFileResult.TabIndex = 24;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(777, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 20);
            this.label13.TabIndex = 23;
            this.label13.Text = ".hmm";
            // 
            // buttonHmmCreateFile
            // 
            this.buttonHmmCreateFile.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHmmCreateFile.Location = new System.Drawing.Point(517, 91);
            this.buttonHmmCreateFile.Name = "buttonHmmCreateFile";
            this.buttonHmmCreateFile.Size = new System.Drawing.Size(162, 43);
            this.buttonHmmCreateFile.TabIndex = 21;
            this.buttonHmmCreateFile.Text = "Create HMM File";
            this.buttonHmmCreateFile.UseVisualStyleBackColor = true;
            this.buttonHmmCreateFile.Click += new System.EventHandler(this.buttonHmmCreateFile_Click);
            // 
            // textHmmFilename
            // 
            this.textHmmFilename.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHmmFilename.Location = new System.Drawing.Point(517, 45);
            this.textHmmFilename.Name = "textHmmFilename";
            this.textHmmFilename.Size = new System.Drawing.Size(254, 32);
            this.textHmmFilename.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(513, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(134, 20);
            this.label12.TabIndex = 22;
            this.label12.Text = "HMM File Name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(150, 20);
            this.label11.TabIndex = 21;
            this.label11.Text = "PFam Domain List";
            // 
            // textHmmDomains
            // 
            this.textHmmDomains.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHmmDomains.HideSelection = false;
            this.textHmmDomains.Location = new System.Drawing.Point(19, 51);
            this.textHmmDomains.Multiline = true;
            this.textHmmDomains.Name = "textHmmDomains";
            this.textHmmDomains.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textHmmDomains.Size = new System.Drawing.Size(442, 83);
            this.textHmmDomains.TabIndex = 21;
            // 
            // buttonHmmClear
            // 
            this.buttonHmmClear.Location = new System.Drawing.Point(550, 353);
            this.buttonHmmClear.Name = "buttonHmmClear";
            this.buttonHmmClear.Size = new System.Drawing.Size(75, 23);
            this.buttonHmmClear.TabIndex = 19;
            this.buttonHmmClear.Text = "Clear";
            this.buttonHmmClear.UseVisualStyleBackColor = true;
            this.buttonHmmClear.Click += new System.EventHandler(this.buttonHmmClear_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(546, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "Results";
            // 
            // textHmmTitle
            // 
            this.textHmmTitle.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHmmTitle.Location = new System.Drawing.Point(26, 49);
            this.textHmmTitle.Name = "textHmmTitle";
            this.textHmmTitle.Size = new System.Drawing.Size(845, 32);
            this.textHmmTitle.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(22, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Title";
            // 
            // textHmmSequence2
            // 
            this.textHmmSequence2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHmmSequence2.HideSelection = false;
            this.textHmmSequence2.Location = new System.Drawing.Point(26, 242);
            this.textHmmSequence2.Multiline = true;
            this.textHmmSequence2.Name = "textHmmSequence2";
            this.textHmmSequence2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textHmmSequence2.Size = new System.Drawing.Size(468, 69);
            this.textHmmSequence2.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 219);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(156, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "FASTA Sequence 2";
            // 
            // buttonHmmscan
            // 
            this.buttonHmmscan.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHmmscan.Location = new System.Drawing.Point(26, 333);
            this.buttonHmmscan.Name = "buttonHmmscan";
            this.buttonHmmscan.Size = new System.Drawing.Size(162, 43);
            this.buttonHmmscan.TabIndex = 13;
            this.buttonHmmscan.Text = "Hmmscan";
            this.buttonHmmscan.UseVisualStyleBackColor = true;
            this.buttonHmmscan.Click += new System.EventHandler(this.buttonHmmscan_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 219);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 20);
            this.label7.TabIndex = 12;
            // 
            // textHmmResults
            // 
            this.textHmmResults.Font = new System.Drawing.Font("Lucida Sans Unicode", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHmmResults.Location = new System.Drawing.Point(550, 132);
            this.textHmmResults.Multiline = true;
            this.textHmmResults.Name = "textHmmResults";
            this.textHmmResults.ReadOnly = true;
            this.textHmmResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textHmmResults.Size = new System.Drawing.Size(321, 215);
            this.textHmmResults.TabIndex = 11;
            // 
            // textHmmSequence
            // 
            this.textHmmSequence.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textHmmSequence.HideSelection = false;
            this.textHmmSequence.Location = new System.Drawing.Point(26, 132);
            this.textHmmSequence.Multiline = true;
            this.textHmmSequence.Name = "textHmmSequence";
            this.textHmmSequence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textHmmSequence.Size = new System.Drawing.Size(468, 64);
            this.textHmmSequence.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "FASTA Sequence 1";
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.webBrowser1);
            this.tabOutput.Location = new System.Drawing.Point(4, 25);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(941, 581);
            this.tabOutput.TabIndex = 1;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // checkExtendedOutput
            // 
            this.checkExtendedOutput.AutoSize = true;
            this.checkExtendedOutput.Location = new System.Drawing.Point(314, 342);
            this.checkExtendedOutput.Name = "checkExtendedOutput";
            this.checkExtendedOutput.Size = new System.Drawing.Size(160, 20);
            this.checkExtendedOutput.TabIndex = 12;
            this.checkExtendedOutput.Text = "Show detailed output";
            this.checkExtendedOutput.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 610);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chromothripsis Simulation Suite";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabChromo.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericProbability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCuts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericRuns)).EndInit();
            this.tabImport.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabTest.ResumeLayout(false);
            this.tabTest.PerformLayout();
            this.tabHmmer.ResumeLayout(false);
            this.tabHmmer.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabOutput.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabChromo;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericRuns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.NumericUpDown numericProbability;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericCuts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox selectChromosome;
        private System.Windows.Forms.TabPage tabImport;
        private System.Windows.Forms.Button buttonCreateNullModel;
        private System.Windows.Forms.Button buttonImportEnsemblSynteny;
        private System.Windows.Forms.Button buttonEnsemblSqlite;
        private System.Windows.Forms.TabPage tabHmmer;
        private System.Windows.Forms.TextBox textHmmSequence;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textHmmResults;
        private System.Windows.Forms.Button buttonHmmscan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textHmmSequence2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textHmmTitle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonHmmClear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textHmmDomains;
        private System.Windows.Forms.Button buttonHmmCreateFile;
        private System.Windows.Forms.TextBox textHmmFilename;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelHmmFileResult;
        private System.Windows.Forms.ComboBox selectModelDatabase;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textNullmodelName;
        private System.Windows.Forms.Button buttonConvertChromosome;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonTestNull;
        private System.Windows.Forms.TabPage tabTest;
        private System.Windows.Forms.TextBox textCREnd;
        private System.Windows.Forms.TextBox textCRStart;
        private System.Windows.Forms.TextBox textCRChromosome;
        private System.Windows.Forms.TextBox textCRRes;
        private System.Windows.Forms.Button buttonChromosomeRead;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button buttonChromoBatch;
        private System.Windows.Forms.CheckBox checkExtendedOutput;
    }
}

