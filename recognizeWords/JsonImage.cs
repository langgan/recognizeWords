using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recognizeWords
{
   public class JsonImage
    {
        public class Probability
        {
            /// <summary>
            ///
            /// </summary>
            public double variance { get; set; }
            /// <summary>
            ///
            /// </summary>
            public double average { get; set; }
            /// <summary>
            ///
            /// </summary>
            public double min { get; set; }
        }

        public class Words_resultItem
        {
            /// <summary>
            /// 精确识别图片上的文字
            /// </summary>
            public string words { get; set; }
            /// <summary>
            ///
            /// </summary>
            public Probability probability { get; set; }
        }

        public class Root
        {
            /// <summary>
            ///
            /// </summary>
            public Int64 log_id { get; set; }
            /// <summary>
            ///
            /// </summary>
            public int direction { get; set; }
            /// <summary>
            ///
            /// </summary>
            public int words_result_num { get; set; }
            /// <summary>
            ///
            /// </summary>
            public List<Words_resultItem> words_result { get; set; }
        }
    }
}
