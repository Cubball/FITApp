import { EnhancedWithAuthHttpService } from '../../shared/services/http-auth.service';
import { HttpFactoryService } from '../../shared/services/http-factory.service';

class ReportService {
  private readonly reportEndpoint = 'api/report';

  constructor(private http: EnhancedWithAuthHttpService) {}

  public async getReport(startDate: string, endDate: string): Promise<string> {
    const blob = await this.http.get<Blob>(this.reportEndpoint, {
      params: {
        startDate,
        endDate
      },
      responseType: 'blob'
    });
    return URL.createObjectURL(blob);
  }
}

export const reportService = new ReportService(new HttpFactoryService().createAuthHttpService());
