import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterOutlet } from '@angular/router';
import { map } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('DoyepAnalyzer');
  protected readonly guid = '6d1ba98e-07b2-458a-8b5a-8ffbd27ade4a';
  protected readonly redirectUri = 'http://localhost:4200/login/callback';
  // protected readonly guid = crypto.randomUUID();

  readonly #httpClient = inject(HttpClient);

  public refresh(): void {
    this.#httpClient.post('/auth/refresh', {}).subscribe();
  }

  readonly loginUrl = toSignal(
    this.#httpClient.get<{ url: string }>(
      `auth/login?deviceId=${this.guid}&redirectUri=${this.redirectUri}`)
      .pipe(map(response => response.url))
  );
}
