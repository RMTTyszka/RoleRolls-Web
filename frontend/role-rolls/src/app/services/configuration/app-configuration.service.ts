import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {firstValueFrom} from 'rxjs';

export const asyncConfigInitializer = () => {
  const configService = inject(AppConfigurationService);
  return configService.loadConfig();
};

export interface AppConfig {
  loggingLevel: 'debug' | 'info' | 'warn' | 'error';
}

@Injectable({
  providedIn: 'root'
})
export class AppConfigurationService {
  private config!: AppConfig;
  private http: HttpClient
  constructor() {
    this.http = inject(HttpClient)
  }

  async loadConfig(): Promise<void> {
    this.config = await firstValueFrom(this.http.get<AppConfig>('/assets/config.json'));
  }

  async reloadConfig(): Promise<void> {
    this.config = await firstValueFrom(this.http.get<AppConfig>('/assets/config.json'));
  }

  get loggingLevel() {
    return this.config.loggingLevel;
  }
}
