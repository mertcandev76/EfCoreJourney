-->Katmanlı Mimari ile Asenkron (Async) Repository Pattern ve Service Pattern kullanılarak yapılan CRUD işlemleri

✅ 1. EntityLayer.Concrete (Zaten Hazır)

➤ Görev: Log tablosunu temsil eden model sınıfı.

public class Log
{
    [Key]
    public int LogID { get; set; }
    public DateTime? LogDate { get; set; }
    [StringLength(100)]
    public string? LogLevel { get; set; }
    public string? Message { get; set; }
    public string? Details { get; set; }
    public string? Source { get; set; }
    public string? User { get; set; }
}

✅ 2. DataAccessLayer.Abstract

➤ Görev: ILogDal interface’i ile Log için CRUD işlemlerini tanımlar.

public interface ILogDal
{
    Task<List<Log>> GetAllAsync();
    Task<Log?> GetByIdAsync(int id);
    Task AddAsync(Log log);
    Task UpdateAsync(Log log);
    Task DeleteAsync(Log log);
}
Bu arayüz, EfLogRepository'nin hangi metotları içermesi gerektiğini belirler.

✅ 3. DataAccessLayer.Repository

➤ Görev: ILogDal'ın implementasyonu, veritabanı işlemlerini EF Core ile yapar.

public class EfLogRepository : ILogDal
{
    private readonly AppDbContext _context;

    public EfLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Log>> GetAllAsync()
    {
        return await _context.Logs.ToListAsync();
    }

    public async Task<Log?> GetByIdAsync(int id)
    {
        return await _context.Logs.FindAsync(id);
    }

    public async Task AddAsync(Log log)
    {
        await _context.Logs.AddAsync(log);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Log log)
    {
        _context.Logs.Update(log);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Log log)
    {
        _context.Logs.Remove(log);
        await _context.SaveChangesAsync();
    }
}

AppDbContext, DbSet<Log> Logs şeklinde yapılandırılmış olmalı.

✅ 4. BusinessLayer.Abstract

➤ Görev: UI ile DAL arasında arayüz görevi görür, Controller buna bağımlı olur.

public interface ILogService
{
    Task<List<Log>> GetAllLogsAsync();
    Task<Log?> GetLogByIdAsync(int id);
    Task AddLogAsync(Log log);
    Task UpdateLogAsync(Log log);
    Task DeleteLogAsync(Log log);
}
Controller bu interface üzerinden çalışır, DAL'a doğrudan bağlı kalmaz.

✅ 5. BusinessLayer.Concrete

➤ Görev: ILogService'in implementasyonu, iş mantığı burada uygulanır.

public class LogManager : ILogService
{
    private readonly ILogDal _logDal;

    public LogManager(ILogDal logDal)
    {
        _logDal = logDal;
    }

    public async Task<List<Log>> GetAllLogsAsync()
    {
        return await _logDal.GetAllAsync();
    }

    public async Task<Log?> GetLogByIdAsync(int id)
    {
        return await _logDal.GetByIdAsync(id);
    }

    public async Task AddLogAsync(Log log)
    {
        // İş kuralı varsa burada uygulanır.
        await _logDal.AddAsync(log);
    }

    public async Task UpdateLogAsync(Log log)
    {
        await _logDal.UpdateAsync(log);
    }

    public async Task DeleteLogAsync(Log log)
    {
        await _logDal.DeleteAsync(log);
    }
}

🔄 Ekstra: UI Katmanı – LogController (opsiyonel ama önemli)

➤ Görev: HTTP isteklerini ILogService aracılığıyla işler.

public class LogController : Controller
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        _logService = logService;
    }

    public async Task<IActionResult> Index()
    {
        var logs = await _logService.GetAllLogsAsync();
        return View(logs);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Log log)
    {
        if (ModelState.IsValid)
        {
            await _logService.AddLogAsync(log);
            return RedirectToAction("Index");
        }
        return View(log);
    }
}

✅ Tablolar Arası İlişki Yoksa:

Bu örnekte Log tablosu bağımsızdır. Bu nedenle:
Navigation property yok.
Include() gibi sorgulara ihtiyaç yok.
İş mantığı sade kalır.

📌 Özet Tablosu

| Katman            | Sınıf             | Görev                               |
| ----------------- | ----------------- | ----------------------------------- |
| EntityLayer       | `Log`             | Tabloyu temsil eder                 |
| DAL.Abstract      | `ILogDal`         | CRUD işlemlerinin imzaları          |
| DAL.Repository    | `EfLogRepository` | CRUD işlemlerinin EF ile uygulanışı |
| Business.Abstract | `ILogService`     | Servis imzaları                     |
| Business.Concrete | `LogManager`      | İş mantığı + DAL erişimi            |
| Controller        | `LogController`   | UI yönlendirmesi, veri akışı        |
