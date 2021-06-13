import {Injectable} from '@angular/core';
import {ActivatedRoute, NavigationEnd, Router} from '@angular/router';
import {filter, map} from 'rxjs/operators';
import {ModuleDataColor} from './shared/ModuleDataColor';
import {DefaultDataColor} from './default-data.color';

@Injectable({
  providedIn: 'root'
})
export class AppColorService {
  private alreadyStarted = false;
  constructor(private router: Router, private activatedRoute: ActivatedRoute) {
  }

  public init(): void {
    if (this.alreadyStarted) {
      return;
    }
    this.alreadyStarted = true;

    this.router.events.pipe(
      filter((event) => event instanceof NavigationEnd),
      map(() => {
        const title = this.getTitle(this.router.routerState, this.router.routerState.root);
        return title ? title : undefined;
      }))
      .subscribe((colorToSet: ModuleDataColor) => {
        this.setColor(colorToSet);
      });
  }

  private getTitle(state: any, parent: any): ModuleDataColor {
    let title: ModuleDataColor;
    if (parent && parent.snapshot.data && parent.snapshot.data.colors) {
      title = parent.snapshot.data.colors;
    }
    if (state && parent) {
      const newTitle = this.getTitle(state, state.firstChild(parent));
      if (newTitle) {
        title = newTitle;
      }
    }
    return title;
  }

  public setColor(color: ModuleDataColor) {
    if (color) {
    } else {
      color = new DefaultDataColor();
    }
    const root = document.documentElement;
    root.style.setProperty('--primary-color', color.primaryColor);
    root.style.setProperty('--secondary-color', color.secondaryColor);
    root.style.setProperty('--danger-color', color.dangerColor);
    root.style.setProperty('--info-color', color.infoColor);
    root.style.setProperty('--warning-color', color.warningColor);
    root.style.setProperty('--color-1', color.color1);
    root.style.setProperty('--color-2', color.color2);
    root.style.setProperty('--color-3', color.color3);
    root.style.setProperty('--color-4', color.color4);
    root.style.setProperty('--color-5', color.color5);
  }
}
