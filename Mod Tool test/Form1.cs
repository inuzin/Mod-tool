using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace Mod_Tool_test
{
    public partial class Form1 : Form
    {
        List<Mod> mods = new List<Mod>();

        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
        BusyForm busyform = new BusyForm();

        String path_real;
        String path_temp;
        String path_modlist;
        String path_modlist_temp;
        String path_mods;
        String path_scriptextender;
        String path_plugins;
        String path_plugins_temp;
        String path_plugins_temp2;

        Process xEdit = new Process();
        Process Loot = new Process();
        Process ini = new Process();
        Process f4se = new Process();
        Process xLODGen = new Process();

        List<String> plugins = new List<String>();

        public Form1()
        {
            InitializeComponent();               

            form3.ShowDialog();

            path_real = form3.path_real;
            path_temp = form3.path_temp;
            path_modlist = form3.path_modlist;
            path_modlist_temp = form3.path_modlist_temp;
            path_mods = form3.path_mods;
            path_scriptextender = form3.path_scriptextender;
            path_plugins = form3.path_plugins;
            path_plugins_temp = form3.path_plugins_temp;
            path_plugins_temp2 = form3.path_plugins_temp2;

            button1.Image = Image.FromFile("D:/Pictures/addicon_modgear.png");
            button2.Image = Image.FromFile("D:/Pictures/minusicon_modgear.png");
            button11.Image = Image.FromFile("D:/Pictures/gearicon_modgear.png");

            //Criar pasta caso nao exista
            System.IO.Directory.CreateDirectory(path_mods);

            //Criar modlist.txt caso nao exista
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_modlist), true))
            {

            }

            //Criar plugins.txt caso nao exista
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_plugins), true))
            {

            }

            //Criar plugins3.txt caso nao exista
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_plugins_temp), true))
            {

            }

            xEdit.StartInfo.FileName = form3.xEditPath;
            Loot.StartInfo.FileName = form3.LootPath;
            ini.StartInfo.FileName = form3.iniPath;
            f4se.StartInfo.FileName = form3.f4sePath;
            xLODGen.StartInfo.FileName = form3.xLODGenPath;

            //Carregar Mods
            CarregarMods();

           
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            //Console.WriteLine(listBox1.SelectedIndex);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void ModBrowser()
        {
            //string[] dirs = Directory.GetDirectories("D:/Mod Tools/Mod tool test/Temporario");

            /*
            foreach (string dir in dirs)
            {
                //listBox1.Items.Add(Path.GetFileName(dir));
            }
            */
        }

        public void Mod(string nome, List<string[]> filepaths)
        {

        }

        public void CarregarMods()
        {
            try
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();

                string[] lines = System.IO.File.ReadAllLines(path_modlist);


                foreach (String l in lines)
                {
                    listBox1.Items.Add(l);
                }

                string[] lines2 = System.IO.File.ReadAllLines(path_plugins);

                foreach (String l in lines2)
                {
                    listBox2.Items.Add(l);                  
                }
            }
            catch
            {

            }


            //RefreshPlugins();
        }

        public void RefreshPlugins()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(path_plugins);



                foreach (String l in lines)
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_plugins_temp2), true))
                    {
                        outputFile.WriteLine(l);
                    }
                }

                File.Delete(path_plugins);
                File.Move(path_plugins_temp2, path_plugins);
            }
            catch
            {

            }

        }

        public void Move(int direction)
        {
            if (listBox1.SelectedItem == null || listBox1.SelectedIndex < 0)
                return;

            int newIndex = listBox1.SelectedIndex + direction;

            if (newIndex < 0 || newIndex >= listBox1.Items.Count)
                return;

            object selected = listBox1.SelectedItem;

            listBox1.Items.Remove(selected);
            listBox1.Items.Insert(newIndex, selected);
            listBox1.SetSelected(newIndex, true);

            
            string[] lines = System.IO.File.ReadAllLines(path_plugins);

            File.Delete(path_modlist);

            for (int b = 0; b <= listBox1.Items.Count; b++)
            {
                try
                {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_modlist), true))
                        {
                            outputFile.WriteLine(listBox1.Items[b]);
                        }                   

                }
                catch
                {

                }

    

                //Console.WriteLine(lines);
            }

            /*
            try
            {
                File.Delete(path_plugins);
                File.Move(path_plugins_temp, path_plugins);
            }
            catch
            {

            }

    */

        }

        private static void processDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                processDirectory(directory);
                if (Directory.GetFiles(directory).Length == 0 &&
                    Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();

            //Local inicial do openfiledialog
            OFD.InitialDirectory = "D:/Downloads";

            //tipos de arquivos que devem ser selecionados
            OFD.Filter = "Addon|*.zip; *.7z; *.rar;";

            OFD.Multiselect = false;

            if (OFD.ShowDialog() == DialogResult.OK)
            {
                //Cria pasta temporaria
                System.IO.Directory.CreateDirectory(path_temp);

                //Extrai os arquivos para a pasta temporaria

                //Caso o arquivo selecionado seja um .zip
                if (OFD.FileName.Contains(".zip") || OFD.FileName.Contains(".rar"))
                {
                    ZipFile.ExtractToDirectory(OFD.FileName, path_temp);
                }
                
                //Caso o arquivo selecionado seja um .7z
                if (OFD.FileName.Contains(".7z"))
                {
                    ProcessStartInfo pro = new ProcessStartInfo();
                    pro.WindowStyle = ProcessWindowStyle.Hidden;
                    pro.FileName = "7za.exe";
                    pro.Arguments = string.Format("x \"{0}\" -y -o\"{1}\"", OFD.FileName, path_temp);
                    Process x = Process.Start(pro);
                    x.WaitForExit();
                }

                //Pega os caminhos na pasta temporaria
                string[] entries = Directory.GetFileSystemEntries(path_temp, "*", SearchOption.AllDirectories);

                //Abrir janela de nome/versao
                //form2.modnome = OFD.FileName;
                form2.ShowDialog();
                

                if (form2.process == true)
                {
                    //busyform.isBusy = true;
                    

                    //mods.Add(new Mod(form2.modnome, entries));

                    
                        //listBox1.Items.Add(form2.modnome);

                    //salva os arquivos do mod no .txt

                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_mods, "" + form2.modnome + ".txt")))
                    {
                        foreach (string entry in entries)
                        {
                            Console.WriteLine("entries qnt: " + entries.Count());
                            //Muda os caminhos para a pasta real
                            string entry1 = entry.Replace(path_temp, path_real);
                            //Salva os caminhos no .txt gerado
                            outputFile.WriteLine(entry1);

                            if (entry1.Contains(".esm") || entry1.Contains(".esp") || entry1.Contains(".esl"))
                            {
                                Console.WriteLine("EAE");
                                Console.WriteLine(entry1);
                                if (entry1.Contains("Fallout 4"))
                                {
                                    using (StreamWriter outputFile2 = new StreamWriter(Path.Combine(path_plugins), true))
                                    {
                                        //String entry2 = entry1.Replace(path_real, "");
                                        String entry2 = entry1.Remove(0, 61);
                                        outputFile2.WriteLine("*" + entry2);
                                        Console.WriteLine(entry2);

                                        mods.Add(new Mod(form2.modnome, entries, entry2));
                                    }
                                }

                                if (entry1.Contains("Skyrim Special Edition"))
                                {
                                    Console.WriteLine("Oiii");
                                    using (StreamWriter outputFile2 = new StreamWriter(Path.Combine(path_plugins), true))
                                    {
                                        //String entry2 = entry1.Replace(path_real, "");
                                        String entry2 = entry1.Remove(0, 74);
                                        outputFile2.WriteLine("*" + entry2);
                                        Console.WriteLine(entry2);

                                        mods.Add(new Mod(form2.modnome, entries, entry2));

                                        using (StreamWriter outputFile3 = new StreamWriter(Path.Combine(path_plugins_temp), true))
                                        {
                                            //outputFile3.WriteLine(entry2);
                                        }
                                    }
                                }
                                


                            }
                        }
                    }

                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_modlist), true))
                        {
                            outputFile.WriteLine(form2.modnome);
                        }

                        /*
                    string[] fileArray = Directory.GetFiles(@"D:\Program Files (x86)\Steam\steamapps\common\Skyrim Special Edition\Data", "*", SearchOption.AllDirectories);

                    foreach(string Newfile in entries)
                    {
                        foreach(string Oldfile in fileArray)
                        {
                            if(Oldfile == Newfile)
                            {
                                Console.WriteLine("" + Oldfile + "==" + Newfile + "");
                                File.Delete(Oldfile);
                                Console.WriteLine("" + Oldfile + "deletado");
                            }
                        }
                    }
                    */

                    //deleta a pasta temporaria
                    Directory.Delete(path_temp, true);

                        File.Delete(path_mods + "" + form2.modnome + ".txt");

                        File.Delete(path_plugins_temp);

                    //Extrai os arquivos para a pasta real

                  

                    //Caso o arquivo selecionado seja um .zip
                    if (OFD.FileName.Contains(".zip") || OFD.FileName.Contains(".rar"))
                    {
                      ZipFile.ExtractToDirectory(OFD.FileName, path_real);
                    }

                    //Caso o arquivo selecionado seja um .7z
                    if (OFD.FileName.Contains(".7z"))
                    {
                    ProcessStartInfo pro = new ProcessStartInfo();
                    pro.WindowStyle = ProcessWindowStyle.Hidden;
                    pro.FileName = "7za.exe";
                    pro.Arguments = string.Format("x \"{0}\" -y -o\"{1}\"", OFD.FileName, path_real);
                    Process x = Process.Start(pro);
                    x.WaitForExit();
                    }


                }


               
            }

            form2.process = false;
            CarregarMods();
            listBox1.Refresh();
            listBox2.Refresh();

            //RefreshPlugins();

            //busyform.isBusy = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("REMOVER MOD");

                string modname = listBox1.GetItemText(listBox1.SelectedItem);

            


                string[] lines = System.IO.File.ReadAllLines(@"" + path_mods + "/" + modname + ".txt");
          

                for (int i = 0; i <= lines.Count() - 1; i++)
                {
                    
                        //Console.WriteLine(lines[i]);

                    //Pega os plugins do mod
                    if (lines[i].Contains(".esp") || lines[i].Contains(".esm") || lines[i].Contains(".esl"))
                    {
                        //61 se FO4 74 se SE
                        //plugins = lines[i].Remove(0, 61);
                        plugins.Add(lines[i].Remove(0, 74));

                        //Console.WriteLine(plugins[i]);
                    }

                // Deleta os arquivos do mod
                try
                {
                    File.Delete(lines[i]);
                }
                catch
                {

                }
                    


                    /*
                        FileAttributes attr = File.GetAttributes(lines[i]);
                    
                        if (attr.HasFlag(FileAttributes.Directory))
                        {
                        if (lines[i].Length == 0)
                        {
                            Directory.Delete(lines[i], true);
                        }
                        }
                        else
                        {
                            File.Delete(lines[i]);
                        }
                        */

                }

            //Deleta diretorios vazios na pasta DATA
            processDirectory(path_real);




            string[] lines1 = System.IO.File.ReadAllLines(path_modlist);

            for (int b = 0; b <= lines1.Count(); b++)
            {
                try
                {

                        if (lines1[b] != modname)
                        {
                            using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_modlist_temp), true))
                            {
                                outputFile.WriteLine(lines1[b]);
                            }
                        }
                    
                }
                catch
                {

                }


                //Console.WriteLine(lines);
            }


            //Deleta o arquivo do mod com as entries
            File.Delete(path_mods + "/" + modname + ".txt");

            //Deleta as linhas do plugins.txt
            /*
            try
             {
                string[] lines2 = System.IO.File.ReadAllLines(path_plugins);


                for (int c = 0; c <= lines2.Count(); c++)
                {
                    
                    if (lines2[c] != "*" + mods[listBox1.SelectedIndex].esp)
                    {
                        using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_plugins_temp2), true))
                        {
                            outputFile.WriteLine(lines2[c]);
                        }
                    }

                }
            }
            catch
            {

            }
           */

            string[] lines2 = System.IO.File.ReadAllLines(path_plugins);

            Boolean writetxt;

            Console.WriteLine("opaaa");

            for(int x = 0; x <= lines2.Count() - 1; x++)
            {
                writetxt = true;

                Console.WriteLine("222222");

                for (int y = 0; y <= plugins.Count() - 1; y++)
                {
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("*" + plugins[y]);

                    //Caso detecte o plugin presente na lista de plugins ativos
                    if (lines2[x] == "*" + plugins[y])
                    {
                        writetxt = false;
                    }
                }

                if (writetxt == true)
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(path_plugins_temp2), true))
                    {
                        outputFile.WriteLine(lines2[x]);
                    }
                }

            }

            try
            {
                File.Delete(path_plugins);
                File.Move(path_plugins_temp2, path_plugins);
            }
            catch
            {

            }


            try
            {
                File.Delete(path_modlist);
                File.Move(path_modlist_temp, path_modlist);
            }
            catch
            {

            }

            listBox1.Items.Remove(listBox1.SelectedItem);           

            CarregarMods();
            listBox1.Refresh();
            listBox2.Refresh();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            xEdit.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Loot.Start();
            if (Loot.Responding == true)
            {
                if (Loot.HasExited == true)
                {
                    listBox2.Refresh();
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ini.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            xLODGen.Start();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            f4se.Start();
            Application.Exit();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Move(-1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Move(1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            Move(-1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Move(1);
        }
        //Options
        private void button11_Click(object sender, EventArgs e)
        {

        }
    }
}
