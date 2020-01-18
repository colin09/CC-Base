using C.B.MySql.Data;
using C.B.MySql.Repository.BaseM;

namespace C.B.MySql.Repository.EntityRepositories {
    //public partial class EntityRepository : BaseRepository<Entity> { }

    public partial class AreaInfoRepository : BaseRepository<AreaInfo> { }
    public partial class NoticeRepository : BaseRepository<Notice> { }
    public partial class NewsInfoRepository : BaseRepository<NewsInfo> { }
    public partial class MessageRepository : BaseRepository<Message> { }
    public partial class HisEventInfoRepository : BaseRepository<HisEventInfo> { }
    public partial class ResourceInfoRepository : BaseRepository<ResourceInfo> { }
    public partial class ExpertInfoRepository : BaseRepository<ExpertInfo> { }
    public partial class EventTypeRepository : BaseRepository<EventType> { }
    public partial class EventInfoRepository : BaseRepository<EventInfo> { }
    public partial class UserInfoRepository : BaseRepository<UserInfo> { }

    public partial class DocumentRepository : BaseRepository<Document> { }

    public partial class AuthNavsRepository : BaseRepository<AuthNavs> { }
    public partial class AuthRoleRepository : BaseRepository<AuthRole> { }
    public partial class AuthUserRepository : BaseRepository<AuthUser> { }
    public partial class AuthRoleNavsRepository : BaseRepository<AuthRoleNavs> { }
    public partial class AuthUserNavsRepository : BaseRepository<AuthUserNavs> { }

}