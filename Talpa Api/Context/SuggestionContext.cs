﻿using Microsoft.EntityFrameworkCore;
using Talpa_Api.Models;

namespace Talpa_Api.Context;

public class SuggestionContext(DbContextOptions<SuggestionContext> options) : DbContext(options)
{
    public DbSet<Suggestion> Suggestions { get; init; } = null!;
}