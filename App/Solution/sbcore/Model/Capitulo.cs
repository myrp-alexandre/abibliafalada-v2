﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using sbcore.Model.Interface;

namespace sbcore.Model
{
    public class Capitulo : ISbItem
    {
        #region Atributos e propriedades
        private IList<Versiculo> versiculos;

        public string Tag { get; set; }
        public int Numero { get; set; }
        public Livro Livro { get; set; }

        public IList<Versiculo> Versiculos
        {
            get
            {
                return new ReadOnlyCollection<Versiculo>(versiculos);
            }
        }
        #endregion

        #region Construtor
        public Capitulo(int numero)
        {
            Numero = numero;
            versiculos = new List<Versiculo>();
        }
        #endregion

        #region Métodos
        public Versiculo AddVersiculo(Versiculo versiculo)
        {
            versiculo.Capitulo = this;
            this.versiculos.Add(versiculo);
            return versiculo;
        }

        public override bool Equals(object obj)
        {
            Capitulo capitulo = obj as Capitulo;
            if ((object)capitulo == null) return false;
            if (!Object.Equals(this.Numero, capitulo.Numero)) return false;
            return true;
        }
        #endregion

        #region ISbItem Members

        public string Display
        {
            get { return Numero.ToString(); }
        }

        public IEnumerable<ISbItem> Children
        {
            get { return Enumerable.Cast<ISbItem>(Versiculos); }
        }

        public ISbItem Parent
        {
            get { return Livro; }
        }

        #endregion
    }
}
