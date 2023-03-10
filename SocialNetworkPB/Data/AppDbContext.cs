using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SocialNetworkPB.Models;

namespace SocialNetworkPB.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Jogo> Jogos { get; set; }
    }
}

