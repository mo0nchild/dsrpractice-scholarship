class AppLocalStorage extends Object {
    public constructor(private readonly storage: Storage) { super(); }
    
    protected storageEventFactory = (key: string, 
            old: string | null, next: string | null): StorageEvent => {
        return new StorageEvent('storage', {
            key: key,
            oldValue: old,
            newValue: next,
            url: window.location.href,
            storageArea: this.storage,
        })
    }
    public setItem(key: string, value: string): void {
        const oldValue = this.storage.getItem(key);
        this.storage.setItem(key, value);
        window.dispatchEvent(this.storageEventFactory(key, oldValue, value));
    }
    public removeItem(key: string): void {
        const oldValue = this.storage.getItem(key);
        this.storage.removeItem(key);
        window.dispatchEvent(this.storageEventFactory(key, oldValue, null));
    }
    public getItem(key: string): string | null {
        return this.storage.getItem(key);
    }
}
export const appStorage = new AppLocalStorage(localStorage);