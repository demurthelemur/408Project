namespace server_v2
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
            this.port_tb = new System.Windows.Forms.TextBox();
            this.numberofQs_tb = new System.Windows.Forms.TextBox();
            this.port_label = new System.Windows.Forms.Label();
            this.numberofQs = new System.Windows.Forms.Label();
            this.listen_button = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.scoreboard = new System.Windows.Forms.RichTextBox();
            this.score_board = new System.Windows.Forms.Label();
            this.logs_txt = new System.Windows.Forms.Label();
            this.start_game_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // port_tb
            // 
            this.port_tb.Location = new System.Drawing.Point(127, 46);
            this.port_tb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.port_tb.Name = "port_tb";
            this.port_tb.Size = new System.Drawing.Size(173, 22);
            this.port_tb.TabIndex = 0;
            this.port_tb.TextChanged += new System.EventHandler(this.port_tb_TextChanged);
            // 
            // numberofQs_tb
            // 
            this.numberofQs_tb.Location = new System.Drawing.Point(127, 116);
            this.numberofQs_tb.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numberofQs_tb.Name = "numberofQs_tb";
            this.numberofQs_tb.Size = new System.Drawing.Size(173, 22);
            this.numberofQs_tb.TabIndex = 1;
            // 
            // port_label
            // 
            this.port_label.AutoSize = true;
            this.port_label.Location = new System.Drawing.Point(36, 50);
            this.port_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.port_label.Name = "port_label";
            this.port_label.Size = new System.Drawing.Size(31, 16);
            this.port_label.TabIndex = 2;
            this.port_label.Text = "Port";
            this.port_label.Click += new System.EventHandler(this.port_label_Click);
            // 
            // numberofQs
            // 
            this.numberofQs.AutoSize = true;
            this.numberofQs.Location = new System.Drawing.Point(17, 119);
            this.numberofQs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberofQs.Name = "numberofQs";
            this.numberofQs.Size = new System.Drawing.Size(91, 16);
            this.numberofQs.TabIndex = 3;
            this.numberofQs.Text = "# of Questions";
            this.numberofQs.Click += new System.EventHandler(this.label2_Click);
            // 
            // listen_button
            // 
            this.listen_button.Location = new System.Drawing.Point(127, 168);
            this.listen_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listen_button.Name = "listen_button";
            this.listen_button.Size = new System.Drawing.Size(175, 28);
            this.listen_button.TabIndex = 4;
            this.listen_button.Text = "Listen";
            this.listen_button.UseVisualStyleBackColor = true;
            this.listen_button.Click += new System.EventHandler(this.listen_button_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(425, 47);
            this.logs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(299, 457);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // scoreboard
            // 
            this.scoreboard.Location = new System.Drawing.Point(40, 279);
            this.scoreboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.scoreboard.Name = "scoreboard";
            this.scoreboard.Size = new System.Drawing.Size(260, 224);
            this.scoreboard.TabIndex = 6;
            this.scoreboard.Text = "";
            // 
            // score_board
            // 
            this.score_board.AutoSize = true;
            this.score_board.Location = new System.Drawing.Point(36, 260);
            this.score_board.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.score_board.Name = "score_board";
            this.score_board.Size = new System.Drawing.Size(83, 16);
            this.score_board.TabIndex = 7;
            this.score_board.Text = "Score Board";
            // 
            // logs_txt
            // 
            this.logs_txt.AutoSize = true;
            this.logs_txt.Location = new System.Drawing.Point(421, 27);
            this.logs_txt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.logs_txt.Name = "logs_txt";
            this.logs_txt.Size = new System.Drawing.Size(37, 16);
            this.logs_txt.TabIndex = 8;
            this.logs_txt.Text = "Logs";
            // 
            // start_game_button
            // 
            this.start_game_button.Enabled = false;
            this.start_game_button.Location = new System.Drawing.Point(127, 220);
            this.start_game_button.Name = "start_game_button";
            this.start_game_button.Size = new System.Drawing.Size(173, 27);
            this.start_game_button.TabIndex = 9;
            this.start_game_button.Text = "Start Game";
            this.start_game_button.UseVisualStyleBackColor = true;
            this.start_game_button.Click += new System.EventHandler(this.start_game_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 554);
            this.Controls.Add(this.start_game_button);
            this.Controls.Add(this.logs_txt);
            this.Controls.Add(this.score_board);
            this.Controls.Add(this.scoreboard);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.listen_button);
            this.Controls.Add(this.numberofQs);
            this.Controls.Add(this.port_label);
            this.Controls.Add(this.numberofQs_tb);
            this.Controls.Add(this.port_tb);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox port_tb;
        private System.Windows.Forms.TextBox numberofQs_tb;
        private System.Windows.Forms.Label port_label;
        private System.Windows.Forms.Label numberofQs;
        private System.Windows.Forms.Button listen_button;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.RichTextBox scoreboard;
        private System.Windows.Forms.Label score_board;
        private System.Windows.Forms.Label logs_txt;
        private System.Windows.Forms.Button start_game_button;
    }
}

