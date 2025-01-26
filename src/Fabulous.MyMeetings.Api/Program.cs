using Fabulous.MyMeetings.Api.Configuration.Authorization;
using Fabulous.MyMeetings.Api.Extensions;

AuthorizationChecker.CheckAllEndpoints();

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

await app.RunAsync();
