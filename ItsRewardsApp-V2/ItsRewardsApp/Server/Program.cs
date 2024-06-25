/*
 * 05/06/2022 ALI - Fixes for allowing for allowing changes 
 */

using ItsRewardsApp.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using ItsRewardsApp.Server.Interfaces;
using ItsRewardsApp.Server.Services;
using ItsRewardsApp.Shared.ViewModels;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddDbContext<LoyaltyUserDBContext>
    (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerLoyaltyDB")));
builder.Services.AddDbContext<LoyaltyBaseDBContext>
    (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoyaltyBaseDB")));
builder.Services.AddTransient<ILoyaltyUserProfile, LoyaltyUserProfileManager>();
builder.Services.AddTransient<IDashboard, Dashboard>();
builder.Services.AddTransient<ILogin, Login>();
builder.Services.AddTransient<IStoreImageService, StoreImageService>();
builder.Services.AddTransient<IQRcode,QRcode>();
builder.Services.AddTransient<IHomeService, HomeService>();
builder.Services.AddTransient<ICoupons, Coupons>();
builder.Services.AddTransient<IResendPin, ResendPINService>();
builder.Services.AddTransient<ISetActiveUser, SetActiveUserService>();
builder.Services.AddTransient<IVerifyActiveUser, VerifyActiveUserService>();
builder.Services.AddTransient<ISearchResult,SearchResultService>();
builder.Services.AddTransient<IEcomCoupons, EcomCouponsService>();
builder.Services.AddTransient<IEcomSearchResult, EcomSearchResultService>();
builder.Services.AddTransient<IStatus,StatusService>();
builder.Services.AddTransient<IpurchaseDetails,PurchaseDetailsService>();
builder.Services.AddTransient<IStorelink, StorelinkService>();
builder.Services.AddSingleton(builder.Configuration.GetSection("Authentication").Get<Authentication>());
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
builder.Services.AddMemoryCache();
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseWebAssemblyDebugging();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller}/{action}");

    endpoints.MapControllers();
    //endpoints.MapRazorPages();

    //endpoints.MapBlazorHub();
    //endpoints.MapFallbackToPage("/_Host");
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
