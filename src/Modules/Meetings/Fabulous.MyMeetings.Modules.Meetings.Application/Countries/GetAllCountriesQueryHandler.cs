using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Countries;

public class GetAllCountriesQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetAllCountriesQuery, IEnumerable<CountryDto>>
{
    public Task<IEnumerable<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            $"""
             SELECT
                [Country].[Code],
                [Country].[Name]
             FROM [Meetings].[v_Countries] AS [Country]
             """;

        return connection.QueryAsync<CountryDto>(sql);
    }
}