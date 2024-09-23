using MinimalApi.Domain.ModelViews;

record LoggedAdminModelView
{
    public int Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Profile { get; set; } = default!;

    public string Token { get; set; } = default!;
}