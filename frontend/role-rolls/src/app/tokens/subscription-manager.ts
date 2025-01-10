import { Subscription } from 'rxjs';

export class SubscriptionManager {
  private subscriptions = new Map<string, Subscription>();
  public add(alias: string, subscription: Subscription) {
    if (this.subscriptions.has(alias)) {
      this.subscriptions.get(alias)?.unsubscribe();
      this.subscriptions.delete(alias);
    }
    this.subscriptions.set(alias, subscription);
  }
  public clear() {
    this.subscriptions.forEach(subscription => {
      subscription.unsubscribe();
    });
  }
}
