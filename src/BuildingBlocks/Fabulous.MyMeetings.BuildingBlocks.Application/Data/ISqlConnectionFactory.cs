using System.Data;

namespace Fabulous.MyMeetings.BuildingBlocks.Application.Data;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}