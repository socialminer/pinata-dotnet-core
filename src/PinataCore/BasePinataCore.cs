using PinataCore.Command;
using PinataCore.Data;
using SocialMiner.Core;
using System.Collections.Generic;

namespace PinataCore
{
    public abstract class BasePinataCore
    {
        protected List<object> SampleData { get; set; }
        protected string[] SamplePath { get; set; }

        protected IDictionary<string, string> DynamicParameters { get; set; }

        protected ICommand Command { get; set; }
        protected IPinataCoreRepository Repository { get; set; }

        protected Options OptionType { get; set; }
        protected Provider.Type Provider { get; set; }

        public BasePinataCore(string connectionString, Provider.Type provider)
        {
            Provider = provider;
            Command = CommandFactory.Create(provider);
            Repository = RepositoryFactory.Create(connectionString, provider);
            DynamicParameters = new Dictionary<string, string>();
        }

        public BasePinataCore(string connectionString, Provider.Type provider, params string[] samplePath)
        {
            Check.Argument.IsNotEmpty(connectionString, nameof(connectionString));

            Provider = provider;
            SamplePath = samplePath;
            
            Command = CommandFactory.Create(provider);
            Repository = RepositoryFactory.Create(connectionString, provider);
            DynamicParameters = new Dictionary<string, string>();
        }

        protected void SetDataFiles(params string[] samplePath)
        {
            SamplePath = samplePath;
        }

        protected void SetDynamicParameters(IDictionary<string, string> parameters)
        {
            DynamicParameters = parameters;
        }

        public abstract void Feed();

        public abstract void Feed(params string[] samplePath);

        public abstract bool Execute(CommandType commandType);

        public abstract bool Execute(CommandType commandType, IDictionary<string, string> parameters);
    }
}