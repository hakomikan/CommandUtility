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

    public abstract class ICommandArgumentConverterAttribute : Attribute, ICommandArgumentConverter
    {
        public abstract Type OutputType { get; }
        public abstract object Convert(string argument);
    }

    public class CommandArgumentConverter
    {
        public CommandArgumentConverter()
        {
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(CommandArgumentConverter).Assembly));
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);

            foreach (var converter in ConverterList)
            {
                Converters[converter.Value.OutputType] = converter.Value;
            }
        }

        public object Convert(Type type, string argument)
        {
            if (Converters.ContainsKey(type))
            {
                try
                {
                    return Converters[type].Convert(argument);
                }
                catch
                {
                    throw new InvalidTypeArgumentException(string.Format("Can't convert: type={0}, value={1}", type.Name, argument));
                }
            }
            else
            {
                throw new InvalidTypeArgumentException(string.Format("Unsupported type: type={0}, value={1}", type.Name, argument));
            }
        }

        private Dictionary<Type, ICommandArgumentConverter> Converters = new Dictionary<Type, ICommandArgumentConverter>();

        private CompositionContainer Container;

        [ImportMany]
        private IEnumerable<Lazy<ICommandArgumentConverter>> ConverterList = null;
    }
}
