using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Persistence.Data.Configurations
{
    public class KnowledgeLibraryConfig :IEntityTypeConfiguration<KnowledgeLibrary>
    {
        public void Configure(EntityTypeBuilder<KnowledgeLibrary> builder)
        {
            builder.HasData(
                new KnowledgeLibrary
                {
                    Id = 1,
                    Title = "Understanding ADHD",
                    Description = "ADHD is not just about focus",
                    Category = "ADHD",
                    ContentType = "Article",
                    CreatedAt = new DateTime(2026,5,14)
                },
                new KnowledgeLibrary
                {
                    Id = 2,
                    Title = "Living with Autism",
                    Description = "Autism is a spectrum of strengths",
                    Category = "Autism",
                    ContentType = "Article",
                    CreatedAt = new DateTime(2026,5,14)
                },
                new KnowledgeLibrary
                {
                    Id = 3,
                    Title = "Understanding OCD",
                    Description = "Obsessive-Compulsive Disorder (OCD) is more than just a need for cleanliness. It is a mental health condition characterized by intrusive thoughts (obsessions) and repetitive behaviors (compulsions) that can significantly impact daily life.",
                    Category = "OCD",
                    ContentType = "Article",
                    CreatedAt = new DateTime(2026,5,14)
                },
                new KnowledgeLibrary
                {
                    Id = 4,
                    Title = "A Guide to Alzheimer's",
                    Description = "Alzheimer's is a progressive brain disorder that affects memory, thinking, and behavior. Early detection and providing a supportive environment are crucial steps in managing the journey for both patients and their families.",
                    Category = "Alzheimer",
                    ContentType = "Video",
                    CreatedAt = new DateTime(2026,5,14)
                }


            );
        }
    }
}
