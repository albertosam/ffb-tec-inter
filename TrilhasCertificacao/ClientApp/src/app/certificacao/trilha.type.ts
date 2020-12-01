import { Certificacao } from './certificacao.type';

export interface Trilha {
    id?: string;
    descricao: string;
    notificar: boolean;
    ativo: boolean;
    ano: number;
    certificacoes: TrilhaCertificacao[];
}

export interface TrilhaCertificacao {
    sequencia: string;
    trilhaId: string;
    certificacaoId: string;
    certificacao: Certificacao;
}
