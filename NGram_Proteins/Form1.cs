using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace NGram_Proteins
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            readAll44Ngrams();

            StreamReader srNgram = new StreamReader("4grams1.csv");
            Stream myStream = null;
            //OpenFileDialog theDialog = new OpenFileDialog();
            //theDialog.Title = "Open Text File";
            //theDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            //theDialog.FilterIndex = 1;
            //// theDialog.Filter = "TXT files|*.txt";
            //theDialog.InitialDirectory = @"C:\";
            string folderSource = textBox1.Text;
            string[] fileNames = Directory.GetFiles(folderSource, "*.*", SearchOption.AllDirectories);
            Hashtable protHash = new Hashtable();
            StreamWriter sw = new StreamWriter("Protein_SVM_4Gm_Oct_10th.csv");
            /*string ngramLine = "";
            while ((ngramLine = srNgram.ReadLine()) != null)
            {
                sw.Write(ngramLine + ",");
            }
            srNgram.Close();
            sw.WriteLine(); */

            int size = -1;
            //  DialogResult result = theDialog.ShowDialog(); // Show the dialog.
            // if (result == DialogResult.OK) // Test result.

            foreach (string file in fileNames)
            {
                //     string file = theDialog.FileName;
                string text = null;
                int nGramSize = 4;
                //try
                //{
                //    text = File.ReadAllText(file);


                //}
                //catch (IOException)
                //{
                //}

                string protein = "";

                string line1;
                //   sw.WriteLine("Protein_SVM" + "," + "Current subject file is  " + file);
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BufferedStream bs = new BufferedStream(fs);

                StreamReader sr = new StreamReader(fs);
                bool startProtFlag = false;
                string proteinString = "";
                int occurence = 0;
                int counter = 0;
                string protNameFinal = "";
                string proteinFinal = "";
                string ngramLine1 = "";
                string lastName = "";               //while ((line1 = sr.ReadLine()) != null)
                //{
                     while (!sr.EndOfStream)// (line1 = sr.ReadLine()) != null)
                {
                    line1 = sr.ReadLine();

                    counter++;
                  //  if (line1.Contains(">"))

                        if (line1.Contains(">") && line1.Length < 15)
                    {
                        startProtFlag = true;
                        lastName = file + "," + protNameFinal;
                        protNameFinal = file+protein;
                        line1 = line1.Remove(0, 1);
                        protein = line1;
                     //   protein = line1.Substring(1, line1.IndexOf('|') - 1);

                        proteinFinal = proteinString;
                        proteinString = "";
                        occurence = 0;
                        if (counter > 2)
                        {
                            goto Analyse;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        proteinString += line1;
                        continue;
                    }
                Analyse:

                        if (protHash.ContainsKey(lastName) == false)
                    {

                        protHash.Add(lastName, proteinFinal);
                        lastName = file + protNameFinal;
                    }
                    /*    occurence = 0;
                        StreamReader srNgram1 = new StreamReader("Ngrams.csv");
                        sw.Write(protNameFinal);
                        while ((ngramLine1 = srNgram1.ReadLine()) != null)
                        {

                            //   string SearchText = "7,true,NA,false:67,false,NA,false:5,false,NA,false:5,false,NA,false";
                            //      string Regex = @"\btrue\b";
                            int NumberOfTrues = Regex.Matches(proteinFinal, ngramLine1).Count;
                            sw.Write(NumberOfTrues + ",");
                            //if (proteinFinal.Contains(ngramLine1))
                            //{
                            //    occurence++;

                            //}

                            //  continue;
                        }
                        srNgram1.Close();
                        sw.WriteLine(); */
                    continue;
                    //    sw.WriteLine(protNameFinal + "," + occurence);
                }

                     if (protHash.ContainsKey(lastName) == false)
                     {

                         protHash.Add(lastName, proteinFinal);
                        // lastName = file + protNameFinal;
                     }


            }

            sw.Write("4gram_Protein" + ",");

            StreamReader srNgram1 = new StreamReader("4grams1.csv");
             //   sw.Write(de.Key + ",");

                //string temp1 = "";
                //int temptotal = 0;
                //string ngramtemp = "";
                string ngramLine2 = "";
                StreamWriter sw2 = new StreamWriter("4grams2.csv");
                while ((ngramLine2 = srNgram1.ReadLine()) != null)
                {
                    int totNumber = 0;
                    foreach (DictionaryEntry de in protHash)
                    {
                        int NumberOfTrues = Regex.Matches(de.Value.ToString(), ngramLine2).Count;
                        totNumber += NumberOfTrues;
                        
                        //temptotal += NumberOfTrues;
                        //temp1 += NumberOfTrues + ",";

                    }

                    if (totNumber > 7)
                    {
                        sw2.WriteLine(ngramLine2);
                        totNumber = 0;
                    }

                }

                sw2.Close();

            //foreach (KeyValuePair<string, int> kvp in nGramHash)
            //{

            //    //if (!test11.Contains(kvp.Key.ToString()))
            //    //{
            //    sw.Write(kvp.Key + ",");
            //    //   }

            //    //   test11 += kvp.Key;
            //}
            //sw.WriteLine();
            string ngramLine = "";

            string ngramsUsed = "";

            StreamReader sr4 = new StreamReader("4grams2.csv");
            string ngramLine5 = "";
            while ((ngramLine5 = sr4.ReadLine()) != null)
            {
                sw.Write(ngramLine5 + ",");
            }
            sw.WriteLine();
            foreach (DictionaryEntry de in protHash)
            {
                srNgram = new StreamReader("4grams2.csv");
             //   sw.Write(de.Key + ",");

                string temp1 = "";
                int temptotal = 0;
                string ngramtemp = "";
                while ((ngramLine = srNgram.ReadLine()) != null)
                {


                    int NumberOfTrues = Regex.Matches(de.Value.ToString(), ngramLine).Count;
                    sw.Write(NumberOfTrues + ",");

                   // temptotal+=NumberOfTrues;
                   // temp1+=NumberOfTrues + ",";

                }
                sw.Write(de.Key.ToString());
                sw.WriteLine();
                //   srNgram.Close();
                //  sw.Write(de.Key.ToString() + ",");
                //if (temptotal > 28)
                //{
                //    sw.WriteLine(de.Key + ","+ temp1);
                //    ngramsUsed += ngramsUsed + ngramLine+ ",";
                //}
                //temp1 = "";
                //temptotal = 0;
                
            }


           // sw.WriteLine(ngramsUsed);
            sw.Close();

            MessageBox.Show("Done");
        }


        private void readAll4Ngrams()
        {
            Stream myStream = null;
          //  OpenFileDialog theDialog = new OpenFileDialog();
        //    theDialog.Title = "Open Text File";
        //    theDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
        //    theDialog.FilterIndex = 1;
            // theDialog.Filter = "TXT files|*.txt";
        //    theDialog.InitialDirectory = @"C:\";
            string folderSource = textBox1.Text;
            string[] fileNames = Directory.GetFiles(folderSource, "*.*", SearchOption.AllDirectories);
            SortedDictionary<string, int> h1 = new SortedDictionary<string, int>();
            StreamWriter sw = new StreamWriter("4grams1.csv");

            int size = -1;
         //   DialogResult result = theDialog.ShowDialog(); // Show the dialog.
            //if (result == DialogResult.OK) // Test result.
            //{
                foreach( string file in fileNames){
                string text = null;
                int nGramSize = 4;
                //try
                //{
                //    text = File.ReadAllText(file);


                //}
                //catch (IOException)
                //{
                //}

                string protein = "";

                string line1;
              //  sw.WriteLine("Protein_SVM" + "," + "Current subject file is  " + file);
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BufferedStream bs = new BufferedStream(fs);

                StreamReader sr = new StreamReader(fs);
                bool startProtFlag = false;
                string proteinString = "";
                int counter = 0;
                string proteinFinal = "";
                while ((line1 = sr.ReadLine()) != null)
                {
                    counter++;
                    if (line1.Contains(">") )
                    {
                        
                        startProtFlag = true;
                        protein = line1.Substring(1, line1.IndexOf('|')-1);
                        proteinFinal = proteinString;
                        proteinString = "";
                        //  continue;
                        if (counter > 2)
                        {
                            goto Analyse;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        proteinString += line1;
                        continue;
                    }
                Analyse:
                    byte size1 = 4;
               // Hashtable h1 = new Hashtable();
              
                string gram = "";
                string tempGram = "";
                    foreach (string listItem in Class1.makeNgrams1(
                proteinFinal, size1))
                    {

                    //    listItem = listItem.Trim();
                        //  documentVectorSpace.Add(listItem);
                        
                        gram = listItem.Trim();
                        
                        //if (gram == "MPL")
                        //{
                        //    int t = 10;
                        //}
                        bool testCase = false;
                        foreach (char c in gram)
                        {
                            if (char.IsNumber(c) || char.IsLower(c))
                            {
                                testCase = true;
                            }
                        }
                        if ( testCase==false&& !h1.ContainsKey(gram))
                        {
                            h1.Add(gram, 1);
                          //  sw.WriteLine(gram);
                            
                        }
                     //   tempGram += gram;
                        //    counter++;

                    }


                    // read protein sequences

                    // string[] fileNames = Directory.GetFiles("C:\\Users\\Izzat\\Desktop\\HMubaid\\Prot_1471-2105-7-s4-s12-s2", "*.*", SearchOption.AllDirectories);
                    
                }

                }
              //  string test11 = "";
                foreach (KeyValuePair<string, int> kvp in h1)
                {

                    //if (!test11.Contains(kvp.Key.ToString()))
                    //{
                        sw.WriteLine(kvp.Key);
                 //   }

                 //   test11 += kvp.Key;
                }
                sw.Close();

            
        }
        SortedDictionary<string, int> nGramHash;
        private void readAll3Ngrams()
        {
            Stream myStream = null;
            //  OpenFileDialog theDialog = new OpenFileDialog();
            //    theDialog.Title = "Open Text File";
            //    theDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            //    theDialog.FilterIndex = 1;
            // theDialog.Filter = "TXT files|*.txt";
            //    theDialog.InitialDirectory = @"C:\";
            string folderSource = textBox2.Text;
            string[] fileNames = Directory.GetFiles(folderSource, "*.*", SearchOption.AllDirectories);
            nGramHash = new SortedDictionary<string, int>();
            StreamWriter sw = new StreamWriter("3grams1.csv");
            byte size1=3;
            string gram = "";
            int size = -1;
            //   DialogResult result = theDialog.ShowDialog(); // Show the dialog.
            //if (result == DialogResult.OK) // Test result.
            //{
            foreach (string file in fileNames)
            {
                string text = null;
                int nGramSize = 3;
                //try
                //{
                //    text = File.ReadAllText(file);


                //}
                //catch (IOException)
                //{
                //}

                string protein = "";

                string line1;
                //  sw.WriteLine("Protein_SVM" + "," + "Current subject file is  " + file);
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BufferedStream bs = new BufferedStream(fs);

                StreamReader sr = new StreamReader(fs);
                bool startProtFlag = false;
                string lastName = "";
                string proteinString = "";
                int counter = 0;
                string proteinFinal = "";
                //while ((line1 = sr.ReadLine()) != null )
                //{
                      while (!sr.EndOfStream)// (line1 = sr.ReadLine()) != null)
                {
                    line1 = sr.ReadLine();
                    counter++;
                 //   counter++;
                    if (line1.Contains(">") && line1.Length<15)
                    {

                        startProtFlag = true;
                    //    protein = line1.Substring(1, line1.IndexOf('|') - 1);
                        line1=line1.Remove(0,1);
                        protein = line1;
                        proteinFinal = proteinString;
                        proteinString = "";
                        //  continue;
                        if (counter > 2)
                        {
                            goto Analyse;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        proteinString += line1;
                        continue;
                    }
                Analyse:
                    size1 = 3;
                    // Hashtable h1 = new Hashtable();

                    gram = "";
                    string tempGram = "";
                    lastName = proteinFinal;
                    foreach (string listItem in Class1.makeNgrams1(
                proteinFinal, size1))
                    {

                        //if (listItem == "BSO" || listItem == "IBS")
                        //{
                        //    int t = 9;
                        //}
                        ////    listItem = listItem.Trim();
                        //  documentVectorSpace.Add(listItem);

                        gram = listItem.Trim();

                        //if (gram == "MPL")
                        //{
                        //    int t = 10;
                        //}
                        bool testCase = false;
                        foreach (char c in gram)
                        {
                            if (char.IsNumber(c) || char.IsLower(c))
                            {
                                testCase = true;
                            }
                        }
                        if (testCase == false && !nGramHash.ContainsKey(gram))
                        {
                            nGramHash.Add(gram, 1);
                            //  sw.WriteLine(gram);

                        }
                        //   tempGram += gram;
                        //    counter++;

                    }


                    // read protein sequences

                    // string[] fileNames = Directory.GetFiles("C:\\Users\\Izzat\\Desktop\\HMubaid\\Prot_1471-2105-7-s4-s12-s2", "*.*", SearchOption.AllDirectories);

                    // accomodate last one


                    
                }

                  //    goto Analyse;

                      //if (nGramHash.ContainsKey(lastName) == false)
                      //{

                      //    nGramHash.Add(lastName, 1);
                      //}


                     // size1 = 3;
                      foreach (string listItem in Class1.makeNgrams1(
                      proteinFinal, size1))
                      {

                          //if (listItem == "BSO" || listItem == "IBS")
                          //{
                          //    int t = 9;
                          //}
                          ////    listItem = listItem.Trim();
                          //  documentVectorSpace.Add(listItem);

                          gram = listItem.Trim();

                          //if (gram == "MPL")
                          //{
                          //    int t = 10;
                          //}
                          bool testCase = false;
                          foreach (char c in gram)
                          {
                              if (char.IsNumber(c) || char.IsLower(c))
                              {
                                  testCase = true;
                              }
                          }
                          if (testCase == false && !nGramHash.ContainsKey(gram))
                          {
                              nGramHash.Add(gram, 1);
                              //  sw.WriteLine(gram);

                          }

                      }





            }
            //  string test11 = "";
            foreach (KeyValuePair<string, int> kvp in nGramHash)
            {

                //if (!test11.Contains(kvp.Key.ToString()))
                //{
                sw.WriteLine(kvp.Key);
                //   }

                //   test11 += kvp.Key;
            }
            sw.Close();


        }

        private void readAll44Ngrams()
        {
            Stream myStream = null;
            //  OpenFileDialog theDialog = new OpenFileDialog();
            //    theDialog.Title = "Open Text File";
            //    theDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            //    theDialog.FilterIndex = 1;
            // theDialog.Filter = "TXT files|*.txt";
            //    theDialog.InitialDirectory = @"C:\";
            string folderSource = textBox2.Text;
            string[] fileNames = Directory.GetFiles(folderSource, "*.*", SearchOption.AllDirectories);
            nGramHash = new SortedDictionary<string, int>();
            StreamWriter sw = new StreamWriter("4grams1.csv");
            byte size1 = 4;
            int size = -1;
            //   DialogResult result = theDialog.ShowDialog(); // Show the dialog.
            //if (result == DialogResult.OK) // Test result.
            //{
            foreach (string file in fileNames)
            {
                string text = null;
                int nGramSize = 4;

                string lastName = "";
                //try
                //{
                //    text = File.ReadAllText(file);


                //}
                //catch (IOException)
                //{
                //}

                string protein = "";
                string gram = "";
                string line1;
                //  sw.WriteLine("Protein_SVM" + "," + "Current subject file is  " + file);
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BufferedStream bs = new BufferedStream(fs);

                StreamReader sr = new StreamReader(fs);
                bool startProtFlag = false;
                string proteinString = "";
                int counter = 0;
                string proteinFinal = "";
                while (!sr.EndOfStream)// (line1 = sr.ReadLine()) != null)
                {
                    line1 = sr.ReadLine();
                    counter++;
                    if (line1.Contains(">"))
                    {

                        startProtFlag = true;
                        line1 = line1.Remove(0, 1);
                        protein = line1;
                        proteinFinal = proteinString;
                        proteinString = "";
                        //  continue;
                        if (counter > 2)
                        {
                            goto Analyse;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        proteinString += line1;
                        continue;
                    }
                Analyse:
                    size1 = 4;
                    // Hashtable h1 = new Hashtable();

                    gram = "";
                    string tempGram = "";
                    foreach (string listItem in Class1.makeNgrams1(
                proteinFinal, size1))
                    {

                        //if (listItem == "BSO" || listItem == "IBS")
                        //{
                        //    int t = 9;
                        //}
                        ////    listItem = listItem.Trim();
                        //  documentVectorSpace.Add(listItem);

                        gram = listItem.Trim();

                        //if (gram == "MPL")
                        //{
                        //    int t = 10;
                        //}
                        bool testCase = false;
                        foreach (char c in gram)
                        {
                            if (char.IsNumber(c) || char.IsLower(c))
                            {
                                testCase = true;
                            }
                        }
                        if (testCase == false && !nGramHash.ContainsKey(gram))
                        {
                            nGramHash.Add(gram, 1);
                            //  sw.WriteLine(gram);

                        }
                        //   tempGram += gram;
                        //    counter++;

                    }







                    // read protein sequences

                    // string[] fileNames = Directory.GetFiles("C:\\Users\\Izzat\\Desktop\\HMubaid\\Prot_1471-2105-7-s4-s12-s2", "*.*", SearchOption.AllDirectories);

                }


                foreach (string listItem in Class1.makeNgrams1(
                  proteinFinal, size1))
                {

                    //if (listItem == "BSO" || listItem == "IBS")
                    //{
                    //    int t = 9;
                    //}
                    ////    listItem = listItem.Trim();
                    //  documentVectorSpace.Add(listItem);

                    gram = listItem.Trim();

                    //if (gram == "MPL")
                    //{
                    //    int t = 10;
                    //}
                    bool testCase = false;
                    foreach (char c in gram)
                    {
                        if (char.IsNumber(c) || char.IsLower(c))
                        {
                            testCase = true;
                        }
                    }
                    if (testCase == false && !nGramHash.ContainsKey(gram))
                    {
                        nGramHash.Add(gram, 1);
                        //  sw.WriteLine(gram);

                    }

                }

            }
            //  string test11 = "";
            foreach (KeyValuePair<string, int> kvp in nGramHash)
            {

                //if (!test11.Contains(kvp.Key.ToString()))
                //{
                sw.WriteLine(kvp.Key);
                //   }

                //   test11 += kvp.Key;
            }
            sw.Close();


        }
        private void button2_Click(object sender, EventArgs e)
        {
            readAll3Ngrams();

            StreamReader srNgram = new StreamReader("3grams1.csv");
            Stream myStream = null;
            //OpenFileDialog theDialog = new OpenFileDialog();
            //theDialog.Title = "Open Text File";
            //theDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            //theDialog.FilterIndex = 1;
            //// theDialog.Filter = "TXT files|*.txt";
            //theDialog.InitialDirectory = @"C:\";
            string folderSource = textBox1.Text;
            string[] fileNames = Directory.GetFiles(folderSource, "*.*", SearchOption.AllDirectories);
            SortedDictionary<Protein, string> protHash = new SortedDictionary<Protein, string>();
            StreamWriter sw = new StreamWriter("Protein_SVM_3Gm_Oct_5th.csv");
            /*string ngramLine = "";
            while ((ngramLine = srNgram.ReadLine()) != null)
            {
                sw.Write(ngramLine + ",");
            }
            srNgram.Close();
            sw.WriteLine(); */

            


            int size = -1;
            //  DialogResult result = theDialog.ShowDialog(); // Show the dialog.
            // if (result == DialogResult.OK) // Test result.
            //StreamWriter swn = new StreamWriter("bbb.csv");
            foreach (string file in fileNames)
            {
                //     string file = theDialog.FileName;
                string text = null;
                int nGramSize = 3;
                //try
                //{
                //    text = File.ReadAllText(file);


                //}
                //catch (IOException)
                //{
                //}

                string protein = "";

                string line1;
                //   sw.WriteLine("Protein_SVM" + "," + "Current subject file is  " + file);
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BufferedStream bs = new BufferedStream(fs);

                StreamReader sr = new StreamReader(fs);
                bool startProtFlag = false;
                string proteinString = "";
                int occurence = 0;
                int counter = 0;
                string protNameFinal = "";
                string proteinFinal = "";
                string ngramLine1 = "";
                string lastName = "";

                string className = "";
                string lastclassName = "";

                while (!sr.EndOfStream)// (line1 = sr.ReadLine()) != null)
                {
                    line1 = sr.ReadLine();
                    counter++;
                    lastclassName = className;
                    if (line1.Contains(">") && line1.Length < 15)
                    {
                        
                        className = "";

                        if(line1.EndsWith(" C")){
                            className = "C";
                        }
                        else if(line1.EndsWith(" P")){
                            className = "P";
                        }
                        else if(line1.EndsWith(" N")){
                            className = "N";
                        }
                        else if(line1.EndsWith(" M")){
                            className = "M";
                        }
                        else {
                             className = "X";
                        }

                        
              //          swn.WriteLine(line1);
                        lastName = file+","+protNameFinal;
                        startProtFlag = true;
                       
                      //  protein = line1.Substring(1, line1.IndexOf('|') - 1);
                        line1 = line1.Remove(0, 1);
                        protein = line1;
                        protNameFinal = protein;
                        proteinFinal = proteinString;
                        proteinString = "";
                        occurence = 0;
                        if (counter > 2)
                        {
                            goto Analyse;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        proteinString += line1;
                        continue;
                    }
                Analyse:

                    Protein p1 = new Protein(lastclassName, lastName);
                //    protNameFinal=file+protNameFinal;
                    if (protHash.ContainsKey(p1) == false)
                    {

                        protHash.Add(p1, proteinFinal);
                        lastName = lastclassName+ file + protNameFinal;
                    }

                    
                /*    occurence = 0;
                 * 
                    StreamReader srNgram1 = new StreamReader("Ngrams.csv");
                    sw.Write(protNameFinal);
                    while ((ngramLine1 = srNgram1.ReadLine()) != null)
                    {

                        //   string SearchText = "7,true,NA,false:67,false,NA,false:5,false,NA,false:5,false,NA,false";
                        //      string Regex = @"\btrue\b";
                        int NumberOfTrues = Regex.Matches(proteinFinal, ngramLine1).Count;
                        sw.Write(NumberOfTrues + ",");
                        //if (proteinFinal.Contains(ngramLine1))
                        //{
                        //    occurence++;

                        //}

                        //  continue;
                    }
                    srNgram1.Close();
                    sw.WriteLine(); */
                    continue;
                    //    sw.WriteLine(protNameFinal + "," + occurence);
                }
                Protein p2 = new Protein(lastclassName, lastName);
                if (protHash.ContainsKey(p2) == false)
                {

                    protHash.Add(p2, proteinFinal);
                }

              
            }

            //swn.Close();
            sw.Write("3gram_Protein" + ",");

            //StreamWriter sw9 = new StreamWriter("izz.csv");
            //foreach (KeyValuePair<string,string> de in protHash)
            //{
            //    sw9.WriteLine(de.Key.ToString() + ",");
            //}

            //sw9.Close();

            foreach (KeyValuePair<string, int> kvp in nGramHash)
            {

                //if (!test11.Contains(kvp.Key.ToString()))
                //{
                sw.Write(kvp.Key +",");
                //   }

                //   test11 += kvp.Key;
            }

            sw.Write("Class");
            sw.WriteLine();
            string ngramLine = "";


            foreach (KeyValuePair<Protein,string> de in protHash)
            {
                srNgram = new StreamReader("3grams1.csv");
                Protein p = (Protein)de.Key;

                sw.Write(p.PClass + ","+p.PName+",");
            while ((ngramLine = srNgram.ReadLine()) != null)
            {
               
                
                int NumberOfTrues = Regex.Matches(de.Value.ToString(), ngramLine).Count;

                sw.Write(NumberOfTrues + ",");
                       }
         //   srNgram.Close();
              //  sw.Write(de.Key.ToString() + ",");
                sw.Write(de.Key.ToString());
            sw.WriteLine();
            }



            sw.Close();

            MessageBox.Show("Done");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string folderSource = textBox1.Text;
            string[] fileNames = Directory.GetFiles(folderSource, "*.*", SearchOption.AllDirectories);
            SortedDictionary<int, string> protHash= new SortedDictionary<int,string>();
           // Hashtable protHash = new Hashtable();
         //   StreamWriter sw = new StreamWriter("Protein_SVM_3Gm_Oct_5th.csv");
            /*string ngramLine = "";
            while ((ngramLine = srNgram.ReadLine()) != null)
            {
                sw.Write(ngramLine + ",");
            }
            srNgram.Close();
            sw.WriteLine(); */

            int size = -1;
            //  DialogResult result = theDialog.ShowDialog(); // Show the dialog.
            // if (result == DialogResult.OK) // Test result.

            foreach (string file in fileNames)
            {
                //     string file = theDialog.FileName;
                string text = null;
                int nGramSize = 3;
                //try
                //{
                //    text = File.ReadAllText(file);


                //}
                //catch (IOException)
                //{
                //}

                string protein = "";

                string line1;
                //   sw.WriteLine("Protein_SVM" + "," + "Current subject file is  " + file);
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                BufferedStream bs = new BufferedStream(fs);

                StreamReader sr = new StreamReader(fs);
                bool startProtFlag = false;
                string proteinString = "";
                int occurence = 0;
                int counter = 0;
                string protNameFinal = "";
                string proteinFinal = "";
                string ngramLine1 = "";
                Hashtable h2 = new Hashtable();
                int generalcounter=0;
                while ((line1 = sr.ReadLine()) != null)
                {
                    counter++;
                    generalcounter++;
                    if (line1.Contains(">") && line1.Length < 15)
                    {
                        protHash.Add(counter, line1);
                        
                        continue;
                    }
                    else if (line1.Contains(">") && line1.Length < 20)
                    {
                        int test = 5;
                    }
                    else if (line1.Contains(">") && line1.Length>20){
                        int test = 5;
                    }

                }

                StreamWriter sww = new StreamWriter("Izz1.txt");

                foreach (KeyValuePair<int, string> de3 in protHash)
                {
                    sww.WriteLine(de3.Key + "," + de3.Value);
                }

                sww.Close();
            }
        }

       

        
    }
}
