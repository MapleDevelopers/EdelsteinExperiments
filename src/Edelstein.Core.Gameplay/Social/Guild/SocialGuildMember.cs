using Edelstein.Entities.Social;

namespace Edelstein.Core.Gameplay.Social.Guild
{
    public class SocialGuildMember : ISocialGuildMember
    {
        private readonly ISocialGuildManager _manager;
        private readonly ISocialGuild _guild;
        private readonly GuildMember _member;

        public int CharacterID => _member.CharacterID;
        public string CharacterName => _member.CharacterName;
        public int Job => _member.Job;
        public int Level => _member.Level;
        public int Grade => _member.Grade;
        public bool Online => _member.Online;
        public int Commitment => _member.Commitment;

        public SocialGuildMember(
            ISocialGuildManager manager,
            ISocialGuild guild,
            GuildMember member
        )
        {
            _manager = manager;
            _guild = guild;
            _member = member;
        }
    }
}