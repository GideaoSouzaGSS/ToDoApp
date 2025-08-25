import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { LogarUsuarioRequest, LogarUsuarioResponse, RegistrarUsuarioRequest } from './models/auth.model';


// =============================================================================
// SERVIÇO DE AUTENTICAÇÃO
// =============================================================================

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  /**
   * ❗️ **IMPORTANTE:** Substitua pela URL base da sua API.
   * O caminho '/autenticacao' foi adicionado para corresponder às suas rotas.
   */
  private apiUrl = 'https://localhost:5002/autenticacao'; // Ex: https://suaapi.com/autenticacao

  private readonly TOKEN_KEY = 'auth_token';

  constructor(private http: HttpClient, private router: Router) {}

  /**
   * Envia uma requisição para registrar um novo usuário.
   * @param dadosRegistro - O objeto contendo nome de usuário, email e senha.
   * @returns Um Observable que completa quando a requisição é bem-sucedida.
   */
  registrar(dadosRegistro: RegistrarUsuarioRequest): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/registrar-usuario`, dadosRegistro);
  }

  /**
   * Envia uma requisição para logar um usuário.
   * Se o login for bem-sucedido, o token JWT é salvo no localStorage.
   * @param credenciais - O objeto contendo email e senha do usuário.
   * @returns Um Observable contendo a resposta com o token.
   */
  login(credenciais: LogarUsuarioRequest): Observable<LogarUsuarioResponse> {
    return this.http.post<LogarUsuarioResponse>(`${this.apiUrl}/logar-usuario`, credenciais).pipe(
      tap(response => {
        // Efeito colateral: salvar o token após o sucesso da requisição
        if (response && response.token) {
          localStorage.setItem(this.TOKEN_KEY, response.token);
        }
      })
    );
  }

  /**
   * Remove o token de autenticação do localStorage e redireciona para a página de login.
   */
  logout(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    this.router.navigate(['/login']); // Ajuste a rota de logout conforme necessário
  }

  /**
   * Recupera o token de autenticação salvo no localStorage.
   * @returns O token JWT como string, ou null se não existir.
   */
  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  /**
   * Verifica se o usuário está autenticado.
   * A verificação básica é se o token existe no localStorage.
   * @returns `true` se um token existir, `false` caso contrário.
   */
  isAuthenticated(): boolean {
    const token = this.getToken();
    // Para uma verificação mais robusta, você poderia decodificar o token JWT
    // e verificar se ele não expirou.
    return !!token;
  }
}