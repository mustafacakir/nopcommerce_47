import React, { useState, useEffect } from 'react';
import { 
  ShoppingBag, 
  Globe, 
  Smartphone, 
  Code, 
  BarChart3, 
  ArrowRight,
  ChevronRight,
  ShieldCheck,
  Zap,
  Users,
  Layout,
  X,
  Eye,
  CheckCircle2,
  Mail,
  Phone,
  User,
  Lock,
  MapPin,
  TrendingUp,
  Cpu,
  MousePointer2,
  Headphones,
  Check
} from 'lucide-react';
import { motion, AnimatePresence } from 'framer-motion';
import './App.css';

// Import theme assets
import themeEarth from './assets/theme-earth.png';
import themePrism from './assets/theme-prism.png';
import themeHero from './assets/theme-hero.png';

const THEMES_DATA = [
  {
    id: 'earth',
    name: 'Earth',
    subtitle: 'Doğal & Minimalist',
    tags: ['Minimalist', 'Organik', 'Sade'],
    mainImage: themeEarth,
    gallery: [themeEarth],
    accentColor: '#6B7C4E',
    overlayColor: 'rgba(107, 124, 78, 0.75)',
    desc: 'Doğal tonlar ve temiz boşluklar. Organik ürünler ve butik markalar için.'
  },
  {
    id: 'prism',
    name: 'Prism',
    subtitle: 'Modern & Dinamik',
    tags: ['Modern', 'Dinamik', 'Çok Kategorili'],
    mainImage: themePrism,
    gallery: [themePrism],
    accentColor: '#6C2D91',
    overlayColor: 'rgba(108, 45, 145, 0.75)',
    desc: 'Çarpıcı renkler ve güçlü tipografi. Teknoloji ve moda markaları için.'
  },
  {
    id: 'hero',
    name: 'Hero',
    subtitle: 'Premium & Şık',
    tags: ['Premium', 'Lüks', 'Dönüşüm Odaklı'],
    mainImage: themeHero,
    gallery: [themeHero],
    accentColor: '#1A2744',
    overlayColor: 'rgba(26, 39, 68, 0.78)',
    desc: 'Lüks his, yüksek dönüşüm. Premium ürünler ve kurumsal markalar için.'
  },
  {
    id: 'velvet',
    name: 'Velvet',
    subtitle: 'Kadifemsi & Lüks',
    tags: ['Lüks', 'Kadın', 'Moda'],
    mainImage: themeHero,
    gallery: [themeHero],
    accentColor: '#7B2D5E',
    overlayColor: 'rgba(123, 45, 94, 0.78)',
    desc: 'Derin mor tonları ve zarif tipografi. Moda ve aksesuar markaları için.'
  },
  {
    id: 'nova',
    name: 'Nova',
    subtitle: 'Teknoloji & Fütüristik',
    tags: ['Teknoloji', 'Koyu Tema', 'Elektronik'],
    mainImage: themePrism,
    gallery: [themePrism],
    accentColor: '#0F4C9E',
    overlayColor: 'rgba(15, 76, 158, 0.78)',
    desc: 'Koyu zemin üzerine parlak aksanlar. Elektronik ve teknoloji markaları için.'
  },
  {
    id: 'bloom',
    name: 'Bloom',
    subtitle: 'Taze & Feminen',
    tags: ['Feminen', 'Pastel', 'Kozmetik'],
    mainImage: themeEarth,
    gallery: [themeEarth],
    accentColor: '#D4607A',
    overlayColor: 'rgba(212, 96, 122, 0.75)',
    desc: 'Yumuşak pembe tonlar ve havadar tasarım. Kozmetik ve güzellik markaları için.'
  },
  {
    id: 'slate',
    name: 'Slate',
    subtitle: 'Kurumsal & Güvenilir',
    tags: ['Kurumsal', 'B2B', 'Sade'],
    mainImage: themeHero,
    gallery: [themeHero],
    accentColor: '#3D5166',
    overlayColor: 'rgba(61, 81, 102, 0.78)',
    desc: 'Gri-mavi kurumsal palet. B2B ve profesyonel hizmet sektörü için.'
  },
  {
    id: 'amber',
    name: 'Amber',
    subtitle: 'Sıcak & Enerjik',
    tags: ['Enerjik', 'Renkli', 'Genç'],
    mainImage: themePrism,
    gallery: [themePrism],
    accentColor: '#C47A1E',
    overlayColor: 'rgba(196, 122, 30, 0.75)',
    desc: 'Altın amber tonları ve bold tasarım. Genç ve dinamik markalar için.'
  },
  {
    id: 'jade',
    name: 'Jade',
    subtitle: 'Ekolojik & Sürdürülebilir',
    tags: ['Eko', 'Yeşil', 'Sürdürülebilir'],
    mainImage: themeEarth,
    gallery: [themeEarth],
    accentColor: '#2D7A5E',
    overlayColor: 'rgba(45, 122, 94, 0.75)',
    desc: 'Canlı yeşil renk paleti. Sürdürülebilir ve eko-dostu markalar için.'
  },
  {
    id: 'ruby',
    name: 'Ruby',
    subtitle: 'Cesur & Dikkat Çekici',
    tags: ['Bold', 'Yüksek Kontrastlı', 'Promosyon'],
    mainImage: themePrism,
    gallery: [themePrism],
    accentColor: '#B91C1C',
    overlayColor: 'rgba(185, 28, 28, 0.78)',
    desc: 'Kırmızı renk gücü ve yüksek kontrast. Promosyon odaklı mağazalar için.'
  },
  {
    id: 'arctic',
    name: 'Arctic',
    subtitle: 'Saf & Ultra Minimalist',
    tags: ['Ultra Sade', 'Beyaz', 'Minimalist'],
    mainImage: themeEarth,
    gallery: [themeEarth],
    accentColor: '#4A90B8',
    overlayColor: 'rgba(74, 144, 184, 0.72)',
    desc: 'Beyaz zemin ve buz mavisi aksanlar. Premium tek ürün ve koleksiyon mağazaları için.'
  },
  {
    id: 'onyx',
    name: 'Onyx',
    subtitle: 'Karanlık & Sofistike',
    tags: ['Karanlık', 'Sofistike', 'Sanat'],
    mainImage: themeHero,
    gallery: [themeHero],
    accentColor: '#1A1A2E',
    overlayColor: 'rgba(26, 26, 46, 0.85)',
    desc: 'Tam karanlık tema ve altın vurgular. Sanat, müzik ve yaratıcı markalar için.'
  },
];

const Navbar = ({ onPageChange, currentPage, onConsult, onRegister }) => {
  const [isScrolled, setIsScrolled] = useState(false);

  useEffect(() => {
    const handleScroll = () => {
      setIsScrolled(window.scrollY > 20);
    };
    window.addEventListener('scroll', handleScroll);
    return () => window.removeEventListener('scroll', handleScroll);
  }, []);

  return (
    <nav className={`navbar ${isScrolled ? 'scrolled' : ''}`}>
      <div className="nav-container">
        <div className="logo" onClick={() => onPageChange('home')}>
          <span className="text-gradient">Pekin</span>Teknoloji
        </div>
        <div className="nav-links">
          <a href="#services" onClick={(e) => {
            if (currentPage !== 'home') {
              e.preventDefault();
              onPageChange('home');
              setTimeout(() => { document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }); }, 100);
            }
          }}>Hizmetler</a>
          <a href="#" onClick={(e) => { e.preventDefault(); onPageChange('ecommerce'); }}>E-Ticaret</a>
          <a href="#packages" onClick={(e) => {
            e.preventDefault();
            if (currentPage !== 'ecommerce') {
              onPageChange('ecommerce');
              setTimeout(() => { document.getElementById('packages')?.scrollIntoView({ behavior: 'smooth' }); }, 150);
              return;
            }
            document.getElementById('packages')?.scrollIntoView({ behavior: 'smooth' });
          }}>Paketler</a>
          <a href="#themes" onClick={(e) => {
            e.preventDefault();
            if (currentPage !== 'ecommerce') {
              onPageChange('ecommerce');
              setTimeout(() => { document.getElementById('themes')?.scrollIntoView({ behavior: 'smooth' }); }, 150);
              return;
            }
            document.getElementById('themes')?.scrollIntoView({ behavior: 'smooth' });
          }}>Temalar</a>
          <button className="btn-nav" onClick={() => currentPage === 'home' ? onConsult() : onRegister()}>
            {currentPage === 'home' ? 'Danışmanlık Al' : 'Ücretsiz Başlayın'}
          </button>
        </div>
      </div>
    </nav>
  );
};

const ThemeModal = ({ isOpen, theme, onClose }) => {
  if (!isOpen || !theme) return null;

  return (
    <AnimatePresence>
      <motion.div 
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        exit={{ opacity: 0 }}
        className="modal-overlay"
        onClick={onClose}
      >
        <motion.div 
          initial={{ scale: 0.95, opacity: 0 }}
          animate={{ scale: 1, opacity: 1 }}
          exit={{ scale: 0.95, opacity: 0 }}
          className="modal-content theme-modal-content"
          onClick={e => e.stopPropagation()}
        >
          <button className="close-btn" onClick={onClose}><X size={20} /></button>
          
          <div className="modal-header">
            <h2>{theme.name}</h2>
            <div className="flex gap-2 mt-2">
              {theme.tags.map(tag => <span key={tag} className="badge-small">{tag}</span>)}
            </div>
          </div>

          <div className="theme-gallery">
            {theme.gallery.map((img, idx) => (
              <div key={idx} className="gallery-item">
                <div className="gallery-label">
                  {idx === 0 ? 'Ana Sayfa' : 'Detay Görünümü'}
                </div>
                <img src={img} alt={`${theme.name} screenshot ${idx + 1}`} />
              </div>
            ))}
          </div>

          <div className="modal-footer">
            <button className="btn-primary" onClick={() => {
              onClose();
              window.dispatchEvent(new CustomEvent('open-registration'));
            }}>
              Bu Temayı Hemen Deneyin
            </button>
          </div>
        </motion.div>
      </motion.div>
    </AnimatePresence>
  );
};

const ConsultationModal = ({ isOpen, onClose }) => {
  const [formData, setFormData] = useState({ name: '', email: '', phone: '', subject: '', message: '' });
  const [sent, setSent] = useState(false);

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    try {
      const res = await fetch('http://localhost:8085/api/fastregister/consultation', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formData),
      });
      const data = await res.json();
      if (data.success) {
        setSent(true);
        setTimeout(() => { setSent(false); onClose(); setFormData({ name: '', email: '', phone: '', subject: '', message: '' }); }, 2500);
      } else {
        setError(data.message || 'Bir hata oluştu.');
      }
    } catch {
      setError('Sunucuya bağlanılamadı.');
    } finally {
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <AnimatePresence>
      <motion.div
        initial={{ opacity: 0 }} animate={{ opacity: 1 }} exit={{ opacity: 0 }}
        className="modal-overlay" onClick={onClose}
      >
        <motion.div
          initial={{ y: 20, opacity: 0 }} animate={{ y: 0, opacity: 1 }} exit={{ y: 20, opacity: 0 }}
          className="modal-content" onClick={e => e.stopPropagation()}
        >
          <button className="close-btn" onClick={onClose}><X size={20} /></button>
          <div className="modal-header">
            <h2>Ücretsiz Danışmanlık Alın</h2>
            <p>Projenizi anlatın, size özel çözüm önerelim.</p>
          </div>
          {sent ? (
            <div style={{ textAlign: 'center', padding: '2rem 0' }}>
              <CheckCircle2 size={48} style={{ color: '#16A34A', margin: '0 auto 1rem' }} />
              <p style={{ fontWeight: 600 }}>Talebiniz alındı, en kısa sürede dönüş yapacağız!</p>
            </div>
          ) : (
            <form onSubmit={handleSubmit}>
              <div className="form-row">
                <div className="form-group">
                  <label>Ad Soyad</label>
                  <input type="text" required placeholder="Adınız Soyadınız" value={formData.name} onChange={e => setFormData({...formData, name: e.target.value})} />
                </div>
                <div className="form-group">
                  <label>Telefon</label>
                  <input type="tel" required placeholder="05XX XXX XX XX" value={formData.phone} onChange={e => setFormData({...formData, phone: e.target.value})} />
                </div>
              </div>
              <div className="form-group">
                <label>E-posta</label>
                <input type="email" required placeholder="ornek@mail.com" value={formData.email} onChange={e => setFormData({...formData, email: e.target.value})} />
              </div>
              <div className="form-group">
                <label>Konu</label>
                <select required value={formData.subject} onChange={e => setFormData({...formData, subject: e.target.value})} style={{ width: '100%', padding: '12px 16px', borderRadius: '12px', border: '1.5px solid #E2E8F0', fontSize: '0.95rem', fontFamily: 'inherit', background: 'white', color: formData.subject ? '#0F172A' : '#94A3B8' }}>
                  <option value="" disabled>Hizmet seçin...</option>
                  <option value="web">Kurumsal Web Sitesi</option>
                  <option value="mobile">Mobil Uygulama</option>
                  <option value="ecommerce">E-Ticaret</option>
                  <option value="software">Özel Yazılım</option>
                  <option value="consulting">Danışmanlık</option>
                </select>
              </div>
              <div className="form-group">
                <label>Mesajınız</label>
                <textarea required placeholder="Projeniz hakkında kısaca bilgi verin..." value={formData.message} onChange={e => setFormData({...formData, message: e.target.value})} rows={3} style={{ width: '100%', padding: '12px 16px', borderRadius: '12px', border: '1.5px solid #E2E8F0', fontSize: '0.95rem', fontFamily: 'inherit', resize: 'vertical' }} />
              </div>
              {error && <p style={{ color: '#B91C1C', fontSize: '0.875rem', marginTop: '0.5rem' }}>{error}</p>}
              <button type="submit" className="btn-primary btn-full mt-4" disabled={loading}>
                {loading ? 'Gönderiliyor...' : <><span>Danışmanlık Talebini Gönder</span> <ArrowRight size={18} /></>}
              </button>
            </form>
          )}
        </motion.div>
      </motion.div>
    </AnimatePresence>
  );
};

const RegistrationModal = ({ isOpen, onClose }) => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    phone: '',
    password: ''
  });
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    
    try {
      const response = await fetch("http://localhost:8085/api/fastregister/register", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(formData),
      });

      const data = await response.json();

      if (data.success) {
        window.location.href = "http://localhost:8085/admin/";
      } else {
        alert("Hata: " + (data.errors ? data.errors.join(", ") : data.message));
        setLoading(false);
      }
    } catch (err) {
      alert("Sunucuya bağlanılamadı. Lütfen sistemin çalıştığından emin olun.");
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <AnimatePresence>
      <motion.div 
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        exit={{ opacity: 0 }}
        className="modal-overlay"
        onClick={onClose}
      >
        <motion.div 
          initial={{ y: 20, opacity: 0 }}
          animate={{ y: 0, opacity: 1 }}
          exit={{ y: 20, opacity: 0 }}
          className="modal-content"
          onClick={e => e.stopPropagation()}
        >
          <button className="close-btn" onClick={onClose}><X size={20} /></button>
          
          <div className="modal-header">
            <h2>Mağazanızı Açın</h2>
            <p>Saniyeler içinde e-ticaret dünyasına adım atın.</p>
          </div>

          <form onSubmit={handleSubmit}>
            <div className="form-row">
              <div className="form-group">
                <label>Ad</label>
                <input type="text" required placeholder="Adınız" value={formData.firstName} onChange={e => setFormData({...formData, firstName: e.target.value})} />
              </div>
              <div className="form-group">
                <label>Soyad</label>
                <input type="text" required placeholder="Soyadınız" value={formData.lastName} onChange={e => setFormData({...formData, lastName: e.target.value})} />
              </div>
            </div>
            
            <div className="form-group">
              <label>E-posta</label>
              <input type="email" required placeholder="ornek@mail.com" value={formData.email} onChange={e => setFormData({...formData, email: e.target.value})} />
            </div>

            <div className="form-group">
              <label>Telefon</label>
              <input type="tel" required placeholder="05XX XXX XX XX" value={formData.phone} onChange={e => setFormData({...formData, phone: e.target.value})} />
            </div>

            <div className="form-group">
              <label>Şifre</label>
              <input type="password" required placeholder="••••••••" value={formData.password} onChange={e => setFormData({...formData, password: e.target.value})} />
            </div>

            <button type="submit" className="btn-primary btn-full mt-4" disabled={loading}>
              {loading ? "Oluşturuluyor..." : "Hemen Mağazamı Oluştur"}
            </button>
          </form>
        </motion.div>
      </motion.div>
    </AnimatePresence>
  );
};

const EC_PACKAGES = [
  {
    name: 'Başlangıç',
    price: '1.490',
    desc: 'E-ticarete ilk adımını atmak isteyen küçük işletmeler için.',
    features: [
      { text: '500 ürün', ok: true },
      { text: '5 GB medya depolama', ok: true },
      { text: 'Sınırsız trafik & bant genişliği', ok: true },
      { text: 'SSL sertifikası', ok: true },
      { text: 'Özel domain bağlama', ok: true },
      { text: 'Mobil uyumlu mağaza', ok: true },
      { text: '3 premium tema', ok: true },
      { text: 'Temel SEO ayarları', ok: true },
      { text: 'Sanal POS (iyzico)', ok: true },
      { text: '1 kargo entegrasyonu', ok: true },
      { text: 'Manuel sipariş oluşturma', ok: true },
      { text: 'E-posta desteği', ok: true },
      { text: 'Terk edilmiş sepet otomasyonu', ok: false },
      { text: 'Pazaryeri entegrasyonu', ok: false },
      { text: 'Kampanya & kupon yönetimi', ok: false },
      { text: 'Toplu ürün yönetimi', ok: false },
      { text: 'Mobil uygulama', ok: false },
    ],
  },
  {
    name: 'Profesyonel',
    price: '2.990',
    popular: true,
    desc: 'Büyüyen markalar için pazaryeri entegrasyonu ve otomasyon gücü.',
    features: [
      { text: '10.000 ürün', ok: true },
      { text: '25 GB medya depolama', ok: true },
      { text: 'Sınırsız trafik & bant genişliği', ok: true },
      { text: 'SSL sertifikası', ok: true },
      { text: 'Özel domain bağlama', ok: true },
      { text: 'Mobil uyumlu mağaza', ok: true },
      { text: 'Tüm 12 premium tema', ok: true },
      { text: 'Gelişmiş SEO & blog modülü', ok: true },
      { text: 'Tüm ödeme sistemleri', ok: true },
      { text: '3 kargo entegrasyonu', ok: true },
      { text: 'Manuel sipariş oluşturma', ok: true },
      { text: '7/24 telefon & e-posta desteği', ok: true },
      { text: 'Terk edilmiş sepet otomasyonu', ok: true },
      { text: 'Trendyol & Hepsiburada entegrasyonu', ok: true },
      { text: 'Kampanya & kupon yönetimi', ok: true },
      { text: 'Toplu ürün yönetimi', ok: true },
      { text: 'Mobil uygulama', ok: false },
    ],
  },
  {
    name: 'Kurumsal',
    price: '7.490',
    desc: 'Sınırsız ölçek, özel geliştirme ve maksimum performans.',
    features: [
      { text: 'Sınırsız ürün', ok: true },
      { text: 'Sınırsız medya depolama', ok: true },
      { text: 'Sınırsız trafik & bant genişliği', ok: true },
      { text: 'SSL sertifikası', ok: true },
      { text: 'Özel domain bağlama', ok: true },
      { text: 'Mobil uyumlu mağaza', ok: true },
      { text: 'Özel tema tasarımı', ok: true },
      { text: 'Tam SEO + içerik yönetimi', ok: true },
      { text: 'Tüm ödeme sistemleri', ok: true },
      { text: 'Tüm kargo firmaları', ok: true },
      { text: 'Manuel sipariş oluşturma', ok: true },
      { text: 'Öncelikli 7/24 destek', ok: true },
      { text: 'Terk edilmiş sepet otomasyonu', ok: true },
      { text: 'Tüm pazaryerleri (Trendyol, Hepsiburada, n11, Amazon)', ok: true },
      { text: 'Kampanya & kupon yönetimi', ok: true },
      { text: 'Toplu ürün yönetimi & API erişimi', ok: true },
      { text: 'iOS & Android mobil uygulama', ok: true },
    ],
  },
];

const EC_FEATURES = [
  { icon: <Zap size={22}/>, title: 'Işık Hızında Sayfalar', desc: 'Google PageSpeed en iyi skorlarıyla dönüşüm oranlarınızı zirveye taşıyın.', stat: '0.8s', statLabel: 'Ort. Yüklenme' },
  { icon: <TrendingUp size={22}/>, title: 'Gelişmiş SEO Motoru', desc: 'Arama motorlarında en üst sıralarda yer almanız için her şey dahil.', stat: '%340', statLabel: 'Org. Trafik' },
  { icon: <Cpu size={22}/>, title: 'Akıllı Entegrasyonlar', desc: 'Trendyol, Hepsiburada, n11 ve kargo firmalarıyla tek tıkla bağlanın.', stat: '6+', statLabel: 'Entegrasyon' },
  { icon: <MousePointer2 size={22}/>, title: 'Toplu İşlem Sihirbazı', desc: 'Binlerce ürünü saniyeler içinde güncelleyin, zaman kazanın.', stat: '10x', statLabel: 'Daha Hızlı' },
  { icon: <Smartphone size={22}/>, title: 'Mobil Uygulama', desc: 'Müşterileriniz için iOS ve Android uygulamasıyla satışlarınızı artırın.', stat: '%68', statLabel: 'Mobil Alışveriş' },
  { icon: <Headphones size={22}/>, title: '7/24 Yerli Destek', desc: 'Kritik anlarda Türkçe destek ekibimiz her zaman yanınızda.', stat: '<2dk', statLabel: 'Yanıt Süresi' },
];

const EC_INTEGRATIONS = [
  { name: 'Trendyol', emoji: '🛍️' },
  { name: 'Hepsiburada', emoji: '🛒' },
  { name: 'n11', emoji: '🏪' },
  { name: 'Amazon TR', emoji: '📦' },
  { name: 'iyzico', emoji: '💳' },
  { name: 'Yurtiçi Kargo', emoji: '🚚' },
  { name: 'Aras Kargo', emoji: '📬' },
  { name: 'MNG Kargo', emoji: '🚛' },
];

const EcommercePage = ({ onRegister, onThemeSelect, themesData = THEMES_DATA }) => {
  const [showAllThemes, setShowAllThemes] = useState(false);
  const visibleThemes = showAllThemes ? themesData : themesData.slice(0, 3);

  return (
  <div className="ecommerce-page">

    <section className="ec-hero">
      <div className="ec-hero-bg" />
      <div className="container">
        <motion.div initial={{ opacity: 0, y: 30 }} animate={{ opacity: 1, y: 0 }} transition={{ duration: 0.6 }} className="ec-hero-content">
          <span className="ec-badge"><span className="ec-badge-dot" />Türkiye'nin Yerli E-Ticaret Altyapısı</span>
          <h1 className="ec-hero-title">E-ticarette<br /><span className="text-gradient">güçlü altyapı,</span><br />tam entegrasyon</h1>
          <p className="ec-hero-sub">E-ticaret sitenizi kurun, tüm pazaryerlerine bağlayın. Sipariş, stok ve kargo yönetimi tek panelden. 14 gün ücretsiz deneyin.</p>
          <div className="ec-hero-btns">
            <button className="btn-primary" onClick={onRegister}>Ücretsiz 14 Gün Deneyin <ArrowRight size={18} /></button>
            <button className="btn-ghost" onClick={() => document.getElementById('packages')?.scrollIntoView({ behavior: 'smooth' })}>Paketleri İncele</button>
          </div>
          <div className="ec-hero-stats">
            <div className="ec-stat"><strong>2.500+</strong><span>Aktif Mağaza</span></div>
            <div className="ec-stat-sep" />
            <div className="ec-stat"><strong>%99.9</strong><span>Uptime</span></div>
            <div className="ec-stat-sep" />
            <div className="ec-stat"><strong>0.8s</strong><span>Ort. Yüklenme</span></div>
            <div className="ec-stat-sep" />
            <div className="ec-stat"><strong>7/24</strong><span>Yerli Destek</span></div>
          </div>
        </motion.div>
      </div>
    </section>

    <section className="ec-features">
      <div className="container">
        <div className="section-header">
          <span className="section-label">Platform Özellikleri</span>
          <h2>E-ticarette büyümeniz için her şey</h2>
          <p className="section-desc">Kurulumdan analize, entegrasyondan desteğe kadar tek çatı altında.</p>
        </div>
        <div className="ec-features-grid">
          {EC_FEATURES.map((f, i) => (
            <motion.div key={f.title} initial={{ opacity: 0, y: 20 }} whileInView={{ opacity: 1, y: 0 }} viewport={{ once: true }} transition={{ delay: i * 0.07 }} className="ec-feature-card">
              <div className="ec-feature-top">
                <div className="icon-box">{f.icon}</div>
                <div className="ec-feature-stat"><strong>{f.stat}</strong><span>{f.statLabel}</span></div>
              </div>
              <h3>{f.title}</h3>
              <p>{f.desc}</p>
            </motion.div>
          ))}
        </div>
      </div>
    </section>

    <section className="ec-integrations">
      <div className="container">
        <div className="section-header">
          <span className="section-label">Entegrasyonlar</span>
          <h2>Tüm platformlara anında bağlanın</h2>
          <p className="section-desc">Pazaryerleri, ödeme sistemleri ve kargo firmalarıyla tam entegrasyon.</p>
        </div>
        <div className="ec-int-grid">
          {EC_INTEGRATIONS.map((int) => (
            <div key={int.name} className="ec-int-card">
              <div className="ec-int-icon">{int.emoji}</div>
              <div className="ec-int-name">{int.name}</div>
              <div className="ec-int-status">● Entegre</div>
            </div>
          ))}
        </div>
      </div>
    </section>

    <section id="packages" className="ec-packages">
      <div className="container">
        <div className="section-header">
          <span className="section-label">Paket Seçenekleri</span>
          <h2>İhtiyacınıza uygun paketi seçin</h2>
          <p className="section-desc">14 gün ücretsiz deneyin, kredi kartı gerekmez.</p>
        </div>
        <div className="ec-packages-grid">
          {EC_PACKAGES.map((pkg, i) => (
            <motion.div key={pkg.name} initial={{ opacity: 0, y: 24 }} whileInView={{ opacity: 1, y: 0 }} viewport={{ once: true }} transition={{ delay: i * 0.08 }} className={`ec-package-card ${pkg.popular ? 'popular' : ''}`}>
              {pkg.popular && <div className="ec-popular-badge">En Çok Tercih Edilen</div>}
              <div className="ec-pkg-header">
                <div className="ec-pkg-name">{pkg.name}</div>
                <p className="ec-pkg-desc">{pkg.desc}</p>
                <div className="ec-pkg-price">
                  <span className="ec-pkg-currency">₺</span>
                  <span className="ec-pkg-amount">{pkg.price}</span>
                  <span className="ec-pkg-period">/ay</span>
                </div>
              </div>
              <div className="ec-pkg-divider" />
              <ul className="ec-pkg-features">
                {pkg.features.map((f) => (
                  <li key={f.text} className={`ec-pkg-feature ${!f.ok ? 'disabled' : ''}`}>
                    <span className={`ec-pkg-check ${f.ok ? 'yes' : 'no'}`}>{f.ok ? '✓' : '×'}</span>
                    {f.text}
                  </li>
                ))}
              </ul>
              <button className={`ec-pkg-btn ${pkg.popular ? 'btn-primary' : 'btn-outline'}`} onClick={onRegister}>
                Hemen Başla <ArrowRight size={16} />
              </button>
            </motion.div>
          ))}
        </div>
      </div>
    </section>

    <section id="themes" className="ec-themes">
      <div className="container">
        <div className="section-header">
          <span className="section-label">Temalar</span>
          <h2>Markanıza özel 12 premium tema</h2>
          <p className="section-desc">Her tema farklı bir iş modeli ve hedef kitleye göre tasarlandı.</p>
        </div>
        <div className="ec-themes-grid-3">
          {visibleThemes.map((theme, i) => (
            <motion.div
              key={theme.id}
              initial={{ opacity: 0, y: 24 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: (i % 3) * 0.1 }}
              className="ec-theme-card-3"
              onClick={() => onThemeSelect?.(theme)}
            >
              <div className="ec-theme-img-wrap-3">
                <img src={theme.mainImage} alt={theme.name} className="ec-theme-img" />
                <div className="ec-theme-color-band" style={{ background: theme.accentColor }} />
                <div className="ec-theme-overlay-3" style={{ background: theme.overlayColor }}>
                  <div className="ec-theme-overlay-inner">
                    <div className="ec-theme-overlay-name">{theme.name}</div>
                    <div className="ec-theme-overlay-desc">{theme.desc}</div>
                    <div className="ec-theme-preview-btn"><Eye size={15} /> Önizle</div>
                  </div>
                </div>
              </div>
              <div className="ec-theme-info-3">
                <div className="ec-theme-info-left">
                  <div className="ec-theme-dot" style={{ background: theme.accentColor }} />
                  <div>
                    <h4>{theme.name}</h4>
                    <span className="ec-theme-subtitle">{theme.subtitle}</span>
                  </div>
                </div>
                <div className="ec-theme-tags-3">
                  {theme.tags.slice(0, 2).map((tag) => (
                    <span key={tag} className="badge-small">{tag}</span>
                  ))}
                </div>
              </div>
            </motion.div>
          ))}
        </div>
        <div className="ec-themes-footer">
          {!showAllThemes && (
            <button className="btn-ghost" onClick={() => setShowAllThemes(true)}>
              Diğer Temaları Keşfet <ChevronRight size={16} />
            </button>
          )}
          {showAllThemes && (
            <button className="btn-ghost" onClick={() => setShowAllThemes(false)}>
              Daha Az Göster
            </button>
          )}
          <button className="btn-primary" onClick={onRegister}>Ücretsiz Deneyin <ArrowRight size={16} /></button>
        </div>
      </div>
    </section>

    <section className="ec-cta">
      <div className="container">
        <motion.div initial={{ opacity: 0, y: 20 }} whileInView={{ opacity: 1, y: 0 }} viewport={{ once: true }} className="ec-cta-card">
          <h2>Hemen başlamak için hazır mısınız?</h2>
          <p>14 gün ücretsiz, kredi kartı gerekmez. Dakikalar içinde mağazanız hazır.</p>
          <button className="btn-primary btn-lg" onClick={onRegister}>Ücretsiz Mağazamı Kur <ArrowRight size={18} /></button>
        </motion.div>
      </div>
    </section>
  </div>
  );
};

const PlatformFeatures = ({ onGoEcommerce }) => {
  const features = [
    { title: 'Işık Hızında Sayfalar', desc: 'Google PageSpeed skorlarında zirvede kalın, dönüşüm oranlarınızı artırın.', icon: <Zap /> },
    { title: 'Gelişmiş SEO Motoru', desc: 'Arama motorlarında en üst sıralarda yer almanız için her şey dahil.', icon: <TrendingUp /> },
    { title: 'Akıllı Entegrasyonlar', desc: 'Pazaryerleri, ödeme sistemleri ve kargo firmalarıyla anında bağlayın.', icon: <Cpu /> },
    { title: 'Toplu İşlem Sihirbazı', desc: 'Binlerce ürünü saniyeler içinde güncelleyin, zaman kazanın.', icon: <MousePointer2 /> },
    { title: 'Mobil Uygulama Gücü', desc: 'Müşterileriniz için yerli iOS ve Android deneyimi sunun.', icon: <Smartphone /> },
    { title: '7/24 Teknik Destek', desc: 'Kritik anlarda her zaman yanınızdayız, profesyonel destek ekibi.', icon: <Headphones /> }
  ];

  return (
    <div className="container">
      <div className="section-header">
        <h2>E-Ticaret Platform Özellikleri</h2>
        <p className="section-desc">E-ticarette büyümeniz için ihtiyacınız olan tüm araçlar tek bir panelde.</p>
      </div>
      <div className="features-grid">
        {features.map((f, idx) => (
          <motion.div 
            key={f.title}
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            transition={{ delay: idx * 0.08 }}
            viewport={{ once: true }}
            className="feature-card"
          >
            <div className="icon-box">{f.icon}</div>
            <h3>{f.title}</h3>
            <p>{f.desc}</p>
          </motion.div>
        ))}
      </div>

      <div className="platform-features-cta">
        <button className="btn-primary" onClick={onGoEcommerce}>
          E‑Ticaret Sayfasını İncele <ArrowRight size={18} />
        </button>
      </div>
    </div>
  );
};

const PricingPage = () => {
  const plans = [
    { name: 'Başlangıç', price: '1.490' },
    { name: 'Profesyonel', price: '2.990', popular: true },
    { name: 'Kurumsal', price: '7.490' }
  ];

  const features = [
    { cat: 'Genel Özellikler' },
    { name: 'Ürün Limiti', values: ['500', '10.000', 'Sınırsız'] },
    { name: 'Depolama Alanı', values: ['5 GB', '25 GB', 'Sınırsız'] },
    { name: 'Hazır Tasarımlar', values: [true, true, true] },
    { cat: 'Pazarlama & SEO' },
    { name: 'Gelişmiş SEO', values: [true, true, true] },
    { name: 'Pazaryeri Entegrasyonları', values: [false, true, true] },
    { name: 'Mobil Uygulama', values: [false, 'Opsiyonel', true] },
    { cat: 'Destek' },
    { name: '7/24 Yerli Destek', values: [true, true, true] },
    { name: 'Özel Danışmanlık', values: [false, false, true] }
  ];

  return (
    <div className="pricing-page pt-32 pb-20">
      <section className="section-padding">
        <div className="container">
          <div className="section-header">
            <span className="text-gradient">Fiyatlandırma</span>
            <h2>Size En Uygun Paketi Seçin</h2>
          </div>
          <div className="pricing-grid mb-20">
            {plans.map(p => (
              <div key={p.name} className={`pricing-card ${p.popular ? 'popular' : ''}`}>
                {p.popular && <div className="popular-badge">En Popüler</div>}
                <h3>{p.name}</h3>
                <div className="price">₺{p.price}<span>/ay</span></div>
                <button className="btn-primary btn-full" onClick={() => window.dispatchEvent(new CustomEvent('open-registration'))}>
                  Hemen Başla
                </button>
              </div>
            ))}
          </div>

          <div className="comparison-header">
            <h2 className="text-3xl font-extrabold mb-4">Tüm Özellikleri Karşılaştırın</h2>
            <p className="text-slate-500 max-w-2xl mx-auto">İhtiyacınıza en uygun paketi seçmek için detaylı özellikleri inceleyin.</p>
          </div>
          
          <div className="table-wrapper mt-12 bg-white shadow-xl rounded-3xl overflow-hidden border border-slate-100">
            <table className="detailed-table w-full text-left">
              <thead>
                <tr className="bg-slate-50">
                  <th className="p-8 text-slate-900 font-bold border-b border-slate-100">Planlar ve Özellikler</th>
                  {plans.map(p => <th key={p.name} className="p-8 text-center text-slate-900 font-bold border-b border-slate-100">{p.name}</th>)}
                </tr>
              </thead>
              <tbody>
                {features.map((f, i) => f.cat ? (
                  <tr key={i} className="bg-slate-50/50">
                    <td colSpan="4" className="px-8 py-5 text-xs font-black uppercase tracking-widest text-blue-600">{f.cat}</td>
                  </tr>
                ) : (
                  <tr key={i} className="hover:bg-slate-50 transition-colors">
                    <td className="px-8 py-6 text-slate-700 font-medium border-b border-slate-50">{f.name}</td>
                    {f.values.map((v, j) => (
                      <td key={j} className="px-8 py-6 text-center border-b border-slate-50">
                        {typeof v === 'boolean' ? (v ? <Check className="mx-auto text-green-500" size={20} /> : <div className="w-5 h-0.5 bg-slate-200 mx-auto" />) : <span className="text-slate-600 font-semibold">{v}</span>}
                      </td>
                    ))}
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>
      </section>
    </div>
  );
};

const ProcessSection = () => {
  const steps = [
    { title: 'Kayıt Olun', desc: 'Bilgilerinizi girerek demoya anında erişim sağlayın.' },
    { title: 'Özelleştirin', desc: 'Temanızı seçin, ürünlerinizi yükleyin ve markanızı yansıtın.' },
    { title: 'Satışa Başlayın', desc: 'Ödeme sisteminizi bağlayın ve dünyaya kapılarınızı açın.' }
  ];

  return (
    <section id="how-it-works" className="section-padding bg-soft">
      <div className="container">
        <div className="section-header">
          <h2>3 Adımda Mağazanız Hazır</h2>
        </div>
        <div className="process-steps">
          {steps.map((s, idx) => (
            <div key={idx} className="process-step">
              <div className="step-number">{idx + 1}</div>
              <h3>{s.title}</h3>
              <p>{s.desc}</p>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

const MapSection = () => (
  <div className="map-container">
    <iframe 
      title="Pekin Teknoloji Ofis"
      src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3009.6231018671686!2d28.9740263!3d41.0260333!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14cab9e7a1620001%3A0xc0fb1d73507119!2sSelanik%20Pasaj%C4%B1!5e0!3m2!1str!2str!4v1700000000000!5m2!1str!2str" 
      width="100%" 
      height="100%" 
      style={{ border: 0 }} 
      allowFullScreen="" 
      loading="lazy"
    ></iframe>
  </div>
);

function App() {
  const [isRegModalOpen, setRegModalOpen] = useState(false);
  const [isConsultModalOpen, setConsultModalOpen] = useState(false);
  const [selectedTheme, setSelectedTheme] = useState(null);
  const [currentPage, setCurrentPage] = useState('home');

  useEffect(() => {
    const handleOpen = () => setRegModalOpen(true);
    window.addEventListener('open-registration', handleOpen);
    window.scrollTo({ top: 0, behavior: 'instant' });
    return () => window.removeEventListener('open-registration', handleOpen);
  }, [currentPage]);

const AgencyServices = ({ onConsult }) => (
  <section id="services" className="services-section">
    <div className="container">
      <div className="section-header">
        <h2>Uçtan Uca Teknoloji Çözümleri</h2>
        <p className="section-desc">Markanızı dijitalde bir adım öne taşıyacak profesyonel hizmetlerimiz.</p>
      </div>
      <div className="services-grid">
        <div className="service-card">
          <div className="service-icon">🌐</div>
          <h3>Kurumsal Web Sitesi</h3>
          <p>Modern, hızlı ve SEO uyumlu kurumsal kimliğinizi en iyi yansıtan profesyonel web çözümleri.</p>
          <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
        </div>
        <div className="service-card">
          <div className="service-icon">📱</div>
          <h3>Mobil Uygulama</h3>
          <p>iOS ve Android platformlarında kullanıcı dostu, yüksek performanslı yerel ve hibrit uygulamalar.</p>
          <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
        </div>
        <div className="service-card">
          <div className="service-icon">⚙️</div>
          <h3>Özel Yazılım</h3>
          <p>İş süreçlerinizi optimize eden, ihtiyaçlarınıza özel terzi usulü yazılım geliştirme hizmetleri.</p>
          <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
        </div>
        <div className="service-card">
          <div className="service-icon">📊</div>
          <h3>Danışmanlık</h3>
          <p>Dijital dönüşüm stratejileri, teknoloji mimarisi ve büyüme odaklı teknoloji danışmanlığı.</p>
          <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
        </div>
      </div>
    </div>
  </section>
);

  const HomePage = () => (
    <>
      {/* Hero Section */}
      <section className="hero hero-centered">
        <motion.div 
          initial={{ opacity: 0, y: 30 }}
          animate={{ opacity: 1, y: 0 }}
          className="hero-content"
        >
          <span className="badge">Geleceği Birlikte İnşa Ediyoruz</span>
          <h1>Teknoloji ve Yazılım <br /> <span className="text-gradient">Çözüm Ortağınız</span></h1>
          <p className="hero-description">
            Kurumsal web çözümlerinden mobil uygulamalara, e-ticaret altyapısından 
            özel yazılım geliştirmeye kadar tüm dijital ihtiyaçlarınız tek bir adreste.
          </p>
          <div className="hero-btns">
            <button className="btn-primary" onClick={() => setConsultModalOpen(true)}>
              Ücretsiz Danışmanlık Al <ArrowRight size={20} />
            </button>
            <a href="#services" className="btn-ghost">Hizmetlerimizi Keşfedin</a>
          </div>
        </motion.div>
      </section>

      <AgencyServices onConsult={() => setConsultModalOpen(true)} />

      <section id="features" className="ecommerce-section">
        <PlatformFeatures onGoEcommerce={() => setCurrentPage('ecommerce')} />
      </section>

      <ProcessSection />

      {/* Contact Section */}
      <section id="contact" className="section-padding bg-soft">
        <div className="container">
          <div className="section-header">
            <h2>Bizimle Tanışın</h2>
          </div>
          
          <div className="contact-grid">
            <div className="glass-card text-center">
              <MapPin className="mx-auto mb-6 text-primary" size={32} />
              <h4 className="font-bold text-lg mb-3">Adres</h4>
              <p className="text-muted">Karamustafa Paşa Mah. Selanik Pasajı No:5 Beyoğlu/İstanbul</p>
            </div>
            <div className="glass-card text-center">
              <Phone className="mx-auto mb-6 text-primary" size={32} />
              <h4 className="font-bold text-lg mb-3">Telefon</h4>
              <p className="text-muted">0850 840 23 36</p>
            </div>
            <div className="glass-card text-center">
              <Mail className="mx-auto mb-6 text-primary" size={32} />
              <h4 className="font-bold text-lg mb-3">E-Posta</h4>
              <p className="text-muted">bilgi@pekinteknoloji.com</p>
            </div>
          </div>

          <MapSection />
        </div>
      </section>
    </>
  );

  return (
    <div className={`app ${currentPage}-view`}>
      <Navbar onPageChange={setCurrentPage} currentPage={currentPage} onConsult={() => setConsultModalOpen(true)} onRegister={() => setRegModalOpen(true)} />
      
      {currentPage === 'home'
        ? <HomePage />
        : currentPage === 'pricing'
          ? <PricingPage />
          : <EcommercePage
            onRegister={() => setRegModalOpen(true)}
            onThemeSelect={setSelectedTheme}
            themesData={THEMES_DATA} 
          />}

      {/* Footer */}
      <footer className="footer">
        <div className="footer-inner">
          <div className="footer-top">
            <div className="footer-info">
              <div className="logo">
                <span className="text-gradient">Pekin</span>Teknoloji
              </div>
              <p>Geleceğin teknolojilerini bugünden inşa ediyor, işinizi dijital dünyada zirveye taşıyoruz.</p>
            </div>
            
            <div className="f-col">
              <h4>Hizmetlerimiz</h4>
              <a href="#services">Kurumsal Web</a>
              <a href="#services">Mobil Uygulama</a>
              <a href="#services">Özel Yazılım</a>
              <a href="#services">Danışmanlık</a>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('ecommerce'); }}>E-Ticaret</a>
            </div>
            
            <div className="f-col">
              <h4>Kurumsal</h4>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('home'); }}>Anasayfa</a>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('ecommerce'); setTimeout(() => document.getElementById('packages')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>Paketler</a>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('ecommerce'); setTimeout(() => document.getElementById('themes')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>Temalar</a>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('home'); setTimeout(() => document.getElementById('how-it-works')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>Nasıl Çalışır?</a>
            </div>

            <div className="f-col">
              <h4>Destek</h4>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('home'); setTimeout(() => document.getElementById('contact')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>İletişim</a>
              <a href="mailto:bilgi@pekinteknoloji.com">Bize Yazın</a>
              <a href="#">KVKK</a>
              <a href="#">Kullanım Şartları</a>
            </div>
          </div>
          
          <div className="footer-bottom">
            <p>© 2026 Pekin Teknoloji. Tüm Hakları Saklıdır.</p>
            <div className="social-links">
              <a href="https://instagram.com/pekinteknoloji" target="_blank" rel="noreferrer">Instagram</a>
              <a href="https://linkedin.com/company/pekinteknoloji" target="_blank" rel="noreferrer">LinkedIn</a>
              <a href="https://twitter.com/pekinteknoloji" target="_blank" rel="noreferrer">Twitter (X)</a>
            </div>
          </div>
        </div>
      </footer>

      <ConsultationModal
        isOpen={isConsultModalOpen}
        onClose={() => setConsultModalOpen(false)}
      />

      <RegistrationModal
        isOpen={isRegModalOpen}
        onClose={() => setRegModalOpen(false)}
      />

      <ThemeModal
        isOpen={!!selectedTheme}
        theme={selectedTheme}
        onClose={() => setSelectedTheme(null)}
      />
    </div>
  );
}

export default App;
