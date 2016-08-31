// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace Lyudmila.Server.Forms
{
    public partial class JsonBuilder : Form
    {
        private static readonly dynamic dynObj = JsonConvert.DeserializeObject(File.ReadAllText("Web\\games.json"));
        private static readonly StringBuilder sb = new StringBuilder();

        public JsonBuilder()
        {
            InitializeComponent();
        }

        /*
         * textBox1 = icon
         * textBox2 = background
         * textBox3 = url
         * textBox4 = filesize
         * textBox5 = description (multiline)
         * 
         * button1 = preview icon
         * button2 = preview bg
         * button3 = save
         */

        private void JsonBuilder_Load(object sender, EventArgs e)
        {
            foreach(var game in dynObj.EnabledGames)
            {
                listBox1.Items.Add(game.Value.longname);
            }
            foreach(var game in dynObj.DisabledGames)
            {
                listBox2.Items.Add(game.Value.longname);
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                LoadValues(listBox1, true);
                listBox2.ClearSelected();
            }
        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if(listBox2.SelectedItem != null)
            {
                LoadValues(listBox2, false);
                listBox1.ClearSelected();
            }
        }


        private void LoadValues(ListBox listbox, bool enabled)
        {
            switch(enabled)
            {
                case true:
                    foreach(var game in dynObj.EnabledGames)
                    {
                        if(game.Value.longname.ToString().Equals(listbox.SelectedItem.ToString()))
                        {
                            textBox1.Text = game.Value.ico;
                            textBox2.Text = game.Value.bgimg;
                            textBox3.Text = game.Value.file;
                            textBox4.Text = game.Value.filesize;
                            textBox5.Text = game.Value.description;
                        }
                    }
                    break;
                case false:
                    foreach(var game in dynObj.DisabledGames)
                    {
                        if(game.Value.longname.ToString().Equals(listbox.SelectedItem.ToString()))
                        {
                            textBox1.Text = game.Value.ico;
                            textBox2.Text = game.Value.bgimg;
                            textBox3.Text = game.Value.file;
                            textBox4.Text = game.Value.filesize;
                            textBox5.Text = game.Value.description;
                        }
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.StartsWith("http://"))
            {
                try
                {
                    Process.Start(textBox1.Text);
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex.GetType()}: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.StartsWith("http://"))
            {
                try
                {
                    Process.Start(textBox2.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.GetType()}: {ex.Message}");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var i = 1;
            Build();

            foreach(var item in listBox1.Items)
            {
                
            }
        }

        private static void Build(string str = null)
        {
            if(str == null)
            {
                sb.Append("{\"EnabledGames\": {\"game1\": { ");
            }
        }
    }
}