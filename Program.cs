var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // 1. Highest Priority: Specific Route
    endpoints.MapControllerRoute(
        name: "UserDetails",
        pattern: "User/Details",
        defaults: new { controller = "User", action = "GetUserDetails" });

    // 2. Higher Priority: Profile route (literal)
    endpoints.MapControllerRoute(
        name: "UserProfile",
        pattern: "User/Profile",
        defaults: new { controller = "User", action = "GetUserProfile" });

    // 3. Integer ID route with constraint
    endpoints.MapControllerRoute(
        name: "UserById",
        pattern: "User/{id:int}",
        defaults: new { controller = "User", action = "GetUserById" });

    // 4. String username route (without constraint)
    endpoints.MapControllerRoute(
        name: "UserByUsername",
        pattern: "User/{username}",
        defaults: new { controller = "User", action = "GetUserByUsername" });

    // 5. Catch-all route for User
    endpoints.MapControllerRoute(
        name: "CatchAllUser",
        pattern: "User/{*wildcard}",
        defaults: new { controller = "User", action = "CatchAllRoutes" });

    // Default route
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");
});

app.Run();
