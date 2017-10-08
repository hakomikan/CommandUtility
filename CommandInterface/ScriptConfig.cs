using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CommandInterface
{
    public abstract class BaseConfig
    {
        /// <summary>
        /// スクリプトの探索ディレクトリ
        /// 以下のように定義します。
        ///     public override string => ProjectRoot.Combine("xxxxx");
        /// </summary>
        public virtual string ScriptPath { get; }
        /// <summary>
        /// ソリューションを生成するときの場所
        /// </summary>
        public abstract string SolutionPath
        {
            get;
        }
        /// <summary>
        /// プロジェクトファイルを生成する時の場所
        /// </summary>
        public abstract string ProjectPath { get; }

        public string SystemRoot = "xxxxx";

        public BaseConfig Load(FileInfo configFile)
        {
            return null;
        }
    }

    public class MyConfig : BaseConfig
    {
        public override string ProjectPath => SystemRoot + "aaa";

        public override string ScriptPath
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public override string SolutionPath => "";

        public Asa()
        {
            this.Sci
        }
    }
}
