// src/app/core/models/auth.model.ts

/**
 * Corresponde ao schema `RegistrarUsuarioRequest` da sua API.
 */
export interface RegistrarUsuarioRequest {
  nomeUsuario: string;
  email: string;
  senha: string;
}

/**
 * Corresponde ao schema `LogarUsuarioRequest` da sua API.
 */
export interface LogarUsuarioRequest {
  email: string;
  senha: string;
}

/**
 * Corresponde ao schema `LogarUsuarioResponse` da sua API.
 */
export interface LogarUsuarioResponse {
  token: string;
}