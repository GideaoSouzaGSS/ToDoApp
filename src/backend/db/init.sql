-- O primeiro bloco de comandos será executado no banco de dados padrão (todo_db)

-- Tabela de ToDos
CREATE TABLE IF NOT EXISTS public."ToDos"
(
    "Id" uuid NOT NULL,
    "ParentId" uuid,
    "Titulo" text COLLATE pg_catalog."default" NOT NULL,
    "Descricao" text COLLATE pg_catalog."default" NOT NULL,
    "DataCriacao" timestamp with time zone NOT NULL,
    "Concluido" boolean NOT NULL,
    "Ativo" boolean NOT NULL,
    CONSTRAINT "PK_ToDos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ToDos_ToDos_ParentId" FOREIGN KEY ("ParentId")
        REFERENCES public."ToDos" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

-- Permissões e índices para a tabela ToDos
ALTER TABLE IF EXISTS public."ToDos"
    OWNER to todo_user;

CREATE INDEX IF NOT EXISTS "IX_ToDos_ParentId"
    ON public."ToDos" USING btree
    ("ParentId" ASC NULLS LAST);

-- ---

-- Mude a conexão para o banco de dados 'todo_db_event'

CREATE DATABASE todo_db_event;

\c todo_db_event



-- O restante dos comandos será executado no banco 'todo_db_event'

-- Tabela de Eventos (Event Store)
CREATE TABLE IF NOT EXISTS public."Eventos"
(
    "Id" uuid NOT NULL,
    "AggregateId" uuid NOT NULL,
    "EventType" text COLLATE pg_catalog."default" NOT NULL,
    "Payload" text COLLATE pg_catalog."default" NOT NULL,
    "Timestamp" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Eventos" PRIMARY KEY ("Id")
);

-- Permissões para a tabela de Eventos
ALTER TABLE IF EXISTS public."Eventos"
    OWNER to todo_user;