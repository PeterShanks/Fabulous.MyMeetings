using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using Fabulous.MyMeetings.Modules.Meetings.Application.Countries;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.Meetings.Countries;

[Route("api/meetings/countries")]
[ApiController]
public class CountriesController(IMeetingsModule meetingsModule) : ControllerBase
{
    [HttpGet("")]
    [HasPermission(MeetingsPermissions.GetMeetingGroupProposals)]
    [ProducesResponseType(typeof(List<CountryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCountries(int? page, int? perPage)
    {
        var countries = await meetingsModule.ExecuteQueryAsync(
            new GetAllCountriesQuery());

        return Ok(countries);
    }
}