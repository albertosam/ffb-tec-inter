﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrilhasCertificacao.Model
{
    public class TrilhaUpdate
    {
        public string Descricao { get; set; }
        public long Ano { get; set; }
        public bool Ativo { get; set; }
        public bool Notificar { get; set; }
        public List<TrilhaCertificacaoCreate> Certificacoes { get; set; }

        public class TrilhaCertificacaoCreate
        {
            public Guid CertificacaoId { get; set; }
        }
    }
}
