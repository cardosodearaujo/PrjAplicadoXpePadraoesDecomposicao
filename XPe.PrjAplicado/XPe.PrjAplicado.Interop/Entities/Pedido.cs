﻿using System.Collections.Generic;
using System;
using XPe.PrjAplicado.Interop.Entities.Base;
using System.Linq;

namespace XPe.PrjAplicado.Interop.Entities
{
    public class Pedido : EntityBase
    {
        public Pedido()
        {
            Codigo = Guid.NewGuid();
        }

        public Guid Codigo { get; set; }
        public Guid CodigoCliente { get; set; }
        public List<PedidoItem> Itens { get; set; }

        public override bool EhValido()
        {
            CodigoClienteEhValido();
            ItensSaoValidos();
            return Mensagens.Any();
        }

        public bool CodigoEhValido()
        {
            if (Codigo == Guid.Empty)
            {
                Mensagens.Add("O código é obrigatório!");
            }

            return Mensagens.Any();
        }

        public bool CodigoClienteEhValido()
        {
            if (CodigoCliente == Guid.Empty)
            {
                Mensagens.Add("O código do cliente é obrigatório!");
            }

            return Mensagens.Any();
        }

        public bool ItensSaoValidos()
        {
            if (Itens is null || !Itens.Any())
            {
                Mensagens.Add("Ao menos um item deve ser adicionado!");
            }
            else
            {
                Itens.ForEach(I =>
                {
                    I.EhValido();
                    Mensagens.AddRange(I.Mensagens);
                });
            }

            return !Mensagens.Any();
        }
    }
}
