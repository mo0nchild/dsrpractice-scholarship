import { useEffect, useState } from "react";
import { appStorage } from "../utils/localstorage";

export default function useLocalStorage(key: string): string | null {
    const [value, setValue] = useState<string | null>(() => {
        return appStorage.getItem(key);
    });
    useEffect(() => {
        const handleStorageChange = (event: StorageEvent) => {
            if (event.key === key) setValue(event.newValue)
        };
        window.addEventListener('storage', handleStorageChange);
        return () => {
            window.removeEventListener('storage', handleStorageChange);
        };
    }, [key]);
    return value;
}