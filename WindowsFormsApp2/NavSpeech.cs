using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace backend.helper
{
    public static class serializer
    {
        public enum Conversiontypes
        {
            xml, json
        }
    }
}

public class Serializer<T> : INotifyPropertyChanged
{
    #region INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler PropertyChanged;
    [NonSerialized]
    private bool _modified;
    [XmlIgnore]
    public bool Modified
    {
        get
        {
            var mod = Modified;
            _modified = false;
            return mod;
        }
    }

    protected void Notify([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        _modified = true;
    }

        #endregion INotifyPropertyChanged Implementation

        public static string GetDefaultFileName()
        {
        return Path.Combine(GetDefaultDirectoryName(), typeof(T).Name);
    }

        public static string GetDefaultFileNameWithExtension()
        {
            return Path.Combine(GetDefaultDirectoryName(), typeof(T).Name + ".json");
        }

        public static string GetDefaultDirectoryName()
    {
        var path = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
        path = Path.GetDirectoryName(path);
        return path;
    }

            public void Save(string fileName = " ", Serializer.ConversionTypes conversionType = Serializer.ConversionTypes.Json)
            {
        if (fileName == null)
            fileName = string.Empty;
        if (string.IsNullOrEmpty(fileName))
            fileName = GetDefaultDirectoryName();
        try
        {
            if (fileName.Contains("file:\\"))
                fileName = fileName.Remove(0, 6);

            var ending = conversionType == Serializer.ConversionTypes.Json ? ".Json" : ".xml";
            var dir = Path.GetDirectoryName(fileName);
            if (fileName.IndexOf(@"\", StringComparison.Ordinal) >= 0)
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);

            if (!Path.HasExtension(fileName))
                fileName = Path.Combine(fileName, typeof(T).Name + ending);
            
            catch
        {

            }
         }

                }


            /*
             * 
             * 1). Wrapper class to store the input from File and Object CMD should search in the file 
             * open Notepad++ and write something into it save and close 
             * Opening CMD is inside the loop (Have to change)
             * Here it starts with CMD (should be able to take multiple arguments) 
             * */


namespace WindowsFormsApp2
{


//public class Word
//    {
//        public Word() { }
//        public string Text { get; set; }          // the word to be recognized by the engine
//        public string AttachedText { get; set; }  // the text associated with the recognized word
//        public bool IsShellCommand { get; set; }  // flag determining whether this word is an command or not

//    }
    public partial class NavSpeech : Form
{
    SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
    SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        //  string[] myCommands = new string[] { "hello", "Dynamics Nav", "cmd", "Notepad++", "Close Notepad"};
        string[] myCommands = File.ReadAllLines(Environment.CurrentDirectory + "\\test.txt");
            

    public NavSpeech()
    {
        InitializeComponent();
    }
        
        // Button Reference
    private void button1_Click(object sender, EventArgs e)
    {
        if (button1.Text.Equals("Enable Voice Control"))
        {
            button1.Text = "Stop Voice Control";
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
        }
        else
        {
           button1.Text = "Enable Voice Control";
            recEngine.RecognizeAsyncStop();
        }

    }

    public void Form1_Load(object sender, EventArgs e)
    {
        Choices commands = new Choices();
        commands.Add(myCommands);
        GrammarBuilder gBuilder = new GrammarBuilder();
        gBuilder.Append(commands);
        Grammar grammar = new Grammar(gBuilder);

        recEngine.LoadGrammarAsync(grammar);
        recEngine.SetInputToDefaultAudioDevice();
        recEngine.SpeechRecognized += recEngine_SpeechRecognized;

    }
    void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
                Process cmd = new Process();
                cmd.StartInfo.FileName = @"notepad++.exe" ;
                cmd.StartInfo.Arguments =@"\Write.txt";
                cmd.Start();
                cmd.CloseMainWindow();
                cmd.WaitForExit();
                cmd.Refresh();
            
            //  if (cmd.StandardError != null)
            // Console.WriteLine(cmd.StandardError.ReadToEnd());


            var result = e.Result;
                var i = 0;
        foreach (var command in myCommands)
        {

                if (command.StartsWith("Stop"))
                    {
                    cmd.StartInfo.FileName = @"notepad++";
                    cmd.Kill();
                }
            if (command.StartsWith("--") || command == string.Empty) continue;  // Skip commentBlocks and skipEmptylines
                var parts = command.Split(new char[] { '|' }); // Split the lines
                       i++;
            if (command.Equals(result.Text))
            {
                Console.WriteLine("Command is  {0}: {1}", i, command);
                break;
            }
        }
    }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
