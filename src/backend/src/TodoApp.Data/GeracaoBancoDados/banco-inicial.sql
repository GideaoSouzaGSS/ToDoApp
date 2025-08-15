CREATE TABLE "Usuarios" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "NomeUsuario" VARCHAR(255) NOT NULL UNIQUE,
    "NomeCompleto" VARCHAR(255),
    "Email" VARCHAR(255) NOT NULL UNIQUE,
    "DataNascimento" DATE,
    "Senha" TEXT NOT NULL,
    "EmailConfirmado" BOOLEAN NOT NULL DEFAULT FALSE,
    "CodigoConfirmacaoEmail" VARCHAR(36),
    "DataGeracaoCodigoEmail" TIMESTAMP,
    "DataCriacao" TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE DATABASE todo_db_event;

\c todo_db_event


CREATE TABLE IF NOT EXISTS public."Eventos"
(
    "Id" uuid NOT NULL,
    "AggregateId" uuid NOT NULL,
    "EventType" text COLLATE pg_catalog."default" NOT NULL,
    "Payload" text COLLATE pg_catalog."default" NOT NULL,
    "Timestamp" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Eventos" PRIMARY KEY ("Id")
);

ALTER TABLE IF EXISTS public."Eventos"
    OWNER to todo_user;