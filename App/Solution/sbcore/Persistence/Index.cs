﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Index;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Db4objects.Db4o;
using sbcore.Model;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;

namespace sbcore.Persistence
{
    public class Index
    {
        private string indexFolderName = string.Empty;

        public Index(string indexFolderName)
        {
            this.indexFolderName = indexFolderName;
        }

        public IndexSearcher GetIndex()
        {
            IndexSearcher searcher = new IndexSearcher(indexFolderName);
            return searcher;
        }

        public void CreateIndex(string databaseFileName)
        {
            IndexWriter writer = new IndexWriter(indexFolderName, new StandardAnalyzer(), true);
            writer.SetUseCompoundFile(false);

            IndexDatabase(writer, Container.GetContainer(databaseFileName));

            writer.Optimize();
            writer.Close();
        }

        private void IndexDatabase(IndexWriter writer, IObjectContainer container)
        {
            IEnumerable<Versiculo> versiculos = container.Query<Versiculo>();
            foreach(Versiculo versiculo in versiculos){
                Document doc = CreateVersiculoDoc(container.Ext().GetID(versiculo), versiculo);
                writer.AddDocument(doc); 
            }
        }

        private Document CreateVersiculoDoc(long id, Versiculo versiculo)
        {
            Document doc = new Document();
            doc.Add(new Field("id", id.ToString(), Field.Store.YES, Field.Index.NO));
            doc.Add(new Field("versiculo", versiculo.Descricao, Field.Store.NO, Field.Index.TOKENIZED));
            return doc;
        }

    }
}
