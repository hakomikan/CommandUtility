using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandInterface
{
    public class CommandSetConfig
    {
        /// <summary>
        /// コマンドセットの名前を設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// スクリプトの探索ディレクトリ
        /// </summary>
        public string ScriptDirectory { get; set; }
        
        /// <summary>
        /// ソリューションやプロジェクトを生成するときの場所
        /// </summary>
        public string ProjectDirecotry { get; set; }
     }
}
