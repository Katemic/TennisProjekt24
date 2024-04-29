using TennisProjekt24.Interfaces;
using TennisProjekt24.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<ICourtService, CourtService>();
builder.Services.AddTransient<IPracticeService, PracticeService>();


builder.Services.AddTransient<IMemberService,MemberService>();
builder.Services.AddTransient<IInstructorService, InstructorService>();
builder.Services.AddTransient<IEventService, EventService>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
