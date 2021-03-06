﻿using Core.Configuration;
using Core.Constants;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class MainContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConfigurationHelper.GetDbContext(DatabaseContexts.MainContext));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountIdentity> AccountIdentities { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<ExamQuestionAnswer> ExamQuestionAnswers { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<SystemMessage> SystemMessages { get; set; }
        public DbSet<SystemMessageType> SystemMessageTypes { get; set; }
        public DbSet<TakenExam> TakenExams { get; set; }
        public DbSet<TakenExamAnswer> TakenExamAnswers { get; set; }
    }
}
