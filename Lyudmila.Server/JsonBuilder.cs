// -----------------------------------------------------------
// Copyrights (c) 2016 Seditio 🍂 INC. All rights reserved.
// -----------------------------------------------------------

using System;
using System.IO;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace Lyudmila.Server
{
    public partial class JsonBuilder : Form
    {
        private static readonly dynamic dynObj = JsonConvert.DeserializeObject(File.ReadAllText("Web\\games.json"));

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
    }
}