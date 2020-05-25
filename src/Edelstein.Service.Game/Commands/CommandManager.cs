using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using Edelstein.Service.Game.Commands.Impl;
using Edelstein.Service.Game.Fields.Objects.User;

namespace Edelstein.Service.Game.Commands
{
    public class CommandManager : AbstractCommand<DefaultCommandContext>
    {
        public const char Prefix = '!';

        public override string Name => "manager";
        public override string Description => "The root command";

        public CommandManager(Parser parser) : base(parser)
        {
            Commands.Add(new StatCommand(parser));
            Commands.Add(new ItemCommand(parser));
            Commands.Add(new ItemPotentialCommand(parser));
            Commands.Add(new FieldCommand(parser));
            Commands.Add(new ContinentCommand(parser));
            Commands.Add(new PartyCommand(parser));
            Commands.Add(new GuildCommand(parser));
        }

        protected override Task Run(FieldUser sender, DefaultCommandContext ctx)
            => Process(sender, new Queue<string>(new[] {"--help"}));
    }
}