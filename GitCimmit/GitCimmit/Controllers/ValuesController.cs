using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace GitCimmit.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            using (Repository repo = new Repository(@"C:\Angular\JSON-Git"))
            {
                string content = "Hello commit!";

                // Create a blob from the content stream
                byte[] contentBytes = System.Text.Encoding.UTF8.GetBytes(content);
                MemoryStream ms = new MemoryStream(contentBytes);
                Blob newBlob = repo.ObjectDatabase.CreateBlob(ms);

                // Put the blob in a tree
                TreeDefinition td = new TreeDefinition();
                td.Add("filePath.txt", newBlob, Mode.NonExecutableFile);
                Tree tree = repo.ObjectDatabase.CreateTree(td);

                // Committer and author
                Signature committer = new Signature("sandiprabade", "sandiprabade171@gmail.com", DateTime.Now);
                Signature author = committer;

                // Create binary stream from the text
                Commit commit = repo.ObjectDatabase.CreateCommit(                    
                    author,
                    committer,
                    "im a commit message :)",
                    tree,
                    repo.Commits,
                    true);

                // Update the HEAD reference to point to the latest commit
                repo.Refs.UpdateTarget(repo.Refs.Head, commit.Id);
            }


            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
