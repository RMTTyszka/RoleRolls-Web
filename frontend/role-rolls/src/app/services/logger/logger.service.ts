import {inject, Injectable, Optional, Provider} from '@angular/core';
import {AppConfigurationService} from '@services/configuration/app-configuration.service';

export interface LoggerConfig {
  useRoot: boolean;
  enabled: boolean;
  logLevel: 'debug' | 'info' | 'warn' | 'error';
}

@Injectable({
  providedIn: 'root'
})
export class LoggerService {
  get logLevel(): "debug" | "info" | "warn" | "error" {
    return this._logLevel;
  }
  private enabled = false;
  private _logLevel: 'debug' | 'info' | 'warn' | 'error' = 'debug';

  constructor(private config: AppConfigurationService) {
    this.enabled = true;
    this._logLevel = config.loggingLevel;
  }
  init(config?: LoggerConfig) {
    if (config) {
      this.configure(config.enabled, config.logLevel);
    }
    const appConfig = inject(AppConfigurationService);
    this.configure(true, appConfig.loggingLevel);
  }

  configure(enableLogging: boolean, level: 'debug' | 'info' | 'warn' | 'error') {
    this.enabled = enableLogging;
    this._logLevel = level;
  }

  isEnabled(): boolean {
    return this.enabled;
  }

  debug(message: string, ...params: any[]) {
    if (this.enabled && this._logLevel === 'debug') {
      console.debug(message, ...params);
    }
  }

  info(message: string, ...params: any[]) {
    if (this.enabled && ['debug', 'info'].includes(this._logLevel)) {
      console.info(message, ...params);
    }
  }

  warn(message: string, ...params: any[]) {
    if (this.enabled && ['debug', 'info', 'warn'].includes(this._logLevel)) {
      console.warn(message, ...params);
    }
  }

  error(message: string, ...params: any[]) {
    if (this.enabled) {
      console.error(message, ...params);
    }
  }
}

export function createLogger(config: LoggerConfig, rootLogger: LoggerService, appConfig: AppConfigurationService): LoggerService {
  if (config.useRoot) {
    return rootLogger;
  }
  const logger = new LoggerService(appConfig);
  logger.init(config);
  return logger;
}

export function provideLogger(config: LoggerConfig): Provider {
  return {
    provide: LoggerService,
    useFactory: (rootLogger: LoggerService, appConfig: AppConfigurationService) => createLogger(config, rootLogger, appConfig),
    deps: [LoggerService, AppConfigurationService],
  };
}
