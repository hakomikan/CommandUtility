using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace CommandUtility
{
    public interface ICommandArgumentConverter
    {
        Type OutputType { get; }
        object Convert(string argument);
    }

    public class CommandArgumentConverter
    {
        public CommandArgumentConverter()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CommandArgumentConverter).Assembly));
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
        }

        public object Convert(Type type, string argument)
        {
            foreach (var converter in Converters)
            {
                if (type == converter.Value.OutputType)
                {
                    try
                    {
                        return converter.Value.Convert(argument);
                    }
                    catch
                    {
                        throw new InvalidTypeArgumentException(string.Format("Can't convert: type={0}, value={1}",type.Name, argument));
                    }
                }
            }

            throw new InvalidTypeArgumentException(string.Format("unsupported type: type={0}, value={1}", type.Name, argument));
        }

        private CompositionContainer Container;

        [ImportMany]
        private IEnumerable<Lazy<ICommandArgumentConverter>> Converters = null;
    }
}
