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
using Newtonsoft.Json;
namespace recognizeWords
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class ApiMessage
        {
            public const string API_ID = "18106051";
            public const string API_KEY = "Z1ihNHwyXknHGrssGZiK864V";
            public const string SECRET_KEY = "RwcdVsTa6fOZoMyofv55IO229iNB5058";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            RecognitionWay(textBox2.Text, "GeneralBasic");
        }
        /// <summary>
        /// 图片上文字识别方式
        /// </summary>
        /// <param name="imgUrl">图片路径</param>
        /// <param name="way">GeneralBasic(普通识别);AccurateBasic(高精度识别)</param>
        private void RecognitionWay(string imgUrl, string way)
        {
            var image = File.ReadAllBytes(imgUrl);
            var client = new Baidu.Aip.Ocr.Ocr(ApiMessage.API_KEY, ApiMessage.SECRET_KEY);
            client.Timeout = 60000;  // 修改超时时间

            var result = client.AccurateBasic(image);
            // 如果有可选参数
            var options = new Dictionary<string, object>{
                {"language_type", "CHN_ENG"},//语言
                {"detect_direction", "true"},//图片方向
                {"probability", "true"}//图片识别成功可能性
                };
            // 带参数调用通用文字识别，识别方式有很多种，我只是取了其中常用的两种，其它方式可以查看官网帮助文档
            switch (way)
            {
                case "AccurateBasic":
                    result = client.AccurateBasic(image, options);
                    break;
                case "GeneralBasic":
                    result = client.GeneralBasic(image, options);
                    break;
            }
            string getJson = result.ToString();　　　　　　　//关键：result返回的是一个一串Json格式的数据，具体大家可以单独输出查看；　　　　　　　//所以要解析这个JSON，还需一个帮助类JsonImage，然后用JSON反序列化，最后StringBuilder拼接
            JsonImage.Root rt = JsonConvert.DeserializeObject<JsonImage.Root>(getJson);//JSON反序列化
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rt.words_result.Count; i++)
            {
                textBox1.Text = sb.AppendLine(rt.words_result[i].words).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] imageFile = Directory.GetFiles(@"G:\新建文件夹");
            //string path = @"G:\新建文件夹\Screenshot_2019-12-25-15-29-57-657_com.miui.home.png";
            int number = imageFile.Length;
            foreach (string str in imageFile)
            {
                File.AppendAllText(@"G:\新建文件夹\1.txt", "*****************************************************************************************************\r\n");
                RecognitionWay(str, "GeneralBasic");
                File.AppendAllText(@"G:\新建文件夹\1.txt", textBox1.Text);

                
               File.AppendAllText(@"G:\新建文件夹\1.txt", "\r\n*****************************************************************************************************\r\n");
                //textBox2.Text += str; 
                //--number;
                //textBox2.Text = number.ToString();
            }
            textBox2.Text ="完成"; 
            //pictureBox1.Image = Image.FromFile(path);
        }
    }
}
