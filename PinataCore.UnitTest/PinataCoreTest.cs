using FluentAssertions;
using PinataCore.Command;
using PinataCore.Data;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PinataCore.UnitTest
{
    public class PinataCoreTest
    {
        protected PinataCore _sutPinata = null;

        public PinataCoreTest()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");

            var connectionStringConfig = builder.Build();

            _sutPinata = new PinataCore(connectionStringConfig.GetConnectionString("soclminer_mysql"), Provider.Type.MySQL);
        }

        public class Execute : PinataCoreTest
        {
            public class Given_A_Insert_Command : IClassFixture<Execute>
            {
                private PinataCore _sutPinata = null;

                public Given_A_Insert_Command(Execute fixture)
                {
                    _sutPinata = fixture._sutPinata;
                    _sutPinata.Feed("Sample/sqlData.json");
                    _sutPinata.Execute(CommandType.Delete);
                }

                [Fact]
                public void When_Execute_Should_Return_True()
                {
                    _sutPinata.Execute(CommandType.Insert).Should().BeTrue();
                }
            }
        }

        public class ExecuteWithParameters : PinataCoreTest
        {
            public class Given_A_Insert_Command : IClassFixture<ExecuteWithParameters>
            {
                private PinataCore _sutPinata = null;
                private IDictionary<string, string> parameters;

                public Given_A_Insert_Command(ExecuteWithParameters fixture)
                {
                    _sutPinata = fixture._sutPinata;
                    _sutPinata.Feed("Sample/sqlDataWithParameters.json");

                    parameters = new Dictionary<string, string>();
                    parameters.Add("movie_01", "The Color Purple");
                    parameters.Add("movie_02", "Lights Out");
                    parameters.Add("movie_03", "The Danish Girl");

                    parameters.Add("actor_01", "Danny Glover");
                    parameters.Add("actor_02", "Whoopi Goldberg");
                    parameters.Add("actor_03", "Eddie Redmayne");
                    parameters.Add("actor_04", "Teresa Palmer");

                    _sutPinata.Execute(CommandType.Delete, parameters);
                }

                [Fact]
                public void When_Execute_Should_Return_True()
                {
                    _sutPinata.Execute(CommandType.Insert, parameters).Should().BeTrue();
                }
            }
        }
    }
}
