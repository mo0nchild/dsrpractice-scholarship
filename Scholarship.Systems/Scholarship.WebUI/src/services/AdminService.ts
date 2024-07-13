import { AxiosResponse } from "axios";
import $api from "../utils/api";

class AdminService {
    public async getBackup(): Promise<AxiosResponse<Blob>> {
        return await $api.get('/backup/get', {
            responseType: 'blob',
        })
    }
    public async loadBackup(data: File): Promise<void> {
        return await $api.post('/backup/load', {
            'backupFile': data
        }, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
    }
}
export const adminService = new AdminService();