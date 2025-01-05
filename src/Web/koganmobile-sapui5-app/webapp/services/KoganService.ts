import { IKoganService } from "./IKoganService";

export default class KoganService implements IKoganService {
    protected urlJoin(baseUrl: string, endpoint: string): string {
        let sFullUri = baseUrl;

        if (!sFullUri.endsWith("/")) {
            sFullUri += "/";
        }

        sFullUri += endpoint.startsWith("/") ? endpoint.substring(1, endpoint.length - 1) : endpoint;

        return sFullUri;
    }
}