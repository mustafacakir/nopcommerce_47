import React, { useState, useEffect } from 'react';
import { Routes, Route, useNavigate, useLocation, useParams, Link } from 'react-router-dom';
import { 
  ShoppingBag,
  Globe,
  Smartphone,
  Code,
  BarChart3,
  ArrowRight,
  ChevronRight,
  ChevronDown,
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
  Check,
  Menu
} from 'lucide-react';
import { motion, AnimatePresence } from 'framer-motion';
import './App.css';

const Logo = ({ size = 'md', light = false }) => (
  <div className={`brand-logo brand-logo--${size}`}>
    <div className="brand-logo-icon">
      <span>P</span>
    </div>
    <div className="brand-logo-text">
      <span className={`brand-logo-name ${light ? 'brand-logo-name--light' : ''}`}>Pekin</span>
      <span className={`brand-logo-suffix ${light ? 'brand-logo-suffix--light' : ''}`}>Teknoloji</span>
    </div>
  </div>
);

const THEMES_DATA = [
  {
    id: 'pioneer',
    name: 'Pioneer',
    subtitle: 'Modern & Çok Amaçlı',
    tags: ["Modern", "Çok Amaçlı", "Dönüşüm Odaklı"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002930_nop-pioneer-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002930_nop-pioneer-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002931_nop-pioneer-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002932_nop-pioneer-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002933_nop-pioneer-responsive-theme.jpeg"],
    accentColor: '#2C3E7A',
    overlayColor: 'rgba(44, 62, 122, 0.78)',
    desc: 'Modern tasarım ve güçlü özellikler. Her sektöre uygun çok amaçlı e-ticaret teması.'
  },
  {
    id: 'voyage',
    name: 'Voyage',
    subtitle: 'Şık & Premium',
    tags: ["Premium", "Şık", "Çok Kategorili"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002898_nop-voyage-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002898_nop-voyage-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002899_nop-voyage-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002900_nop-voyage-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002901_nop-voyage-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002902_nop-voyage-responsive-theme.jpeg"],
    accentColor: '#1A3A5C',
    overlayColor: 'rgba(26, 58, 92, 0.78)',
    desc: 'Zarif ve premium görünüm. Moda, aksesuar ve lifestyle markaları için.'
  },
  {
    id: 'pacific',
    name: 'Pacific',
    subtitle: 'Ferah & Dinamik',
    tags: ["Dinamik", "Ferah", "Moda"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002849_nop-pacific-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002849_nop-pacific-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002850_nop-pacific-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002851_nop-pacific-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002852_nop-pacific-responsive-theme.jpeg"],
    accentColor: '#0077A8',
    overlayColor: 'rgba(0, 119, 168, 0.75)',
    desc: 'Açık ve ferah tasarım. Giyim, spor ve outdoor markaları için.'
  },
  {
    id: 'avenue',
    name: 'Avenue',
    subtitle: 'Kurumsal & Güçlü',
    tags: ["Kurumsal", "Çok Kategorili", "Güçlü"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002646_nop-avenue-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002646_nop-avenue-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002647_nop-avenue-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002648_nop-avenue-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002649_nop-avenue-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002650_nop-avenue-responsive-theme.jpeg"],
    accentColor: '#2D3A4A',
    overlayColor: 'rgba(45, 58, 74, 0.78)',
    desc: 'Kurumsal görünüm, güçlü altyapı. Büyük ölçekli mağazalar için.'
  },
  {
    id: 'emporium',
    name: 'Emporium',
    subtitle: 'Büyük Mağaza & Çarpıcı',
    tags: ["Büyük Mağaza", "Çarpıcı", "Renkli"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002503_nop-emporium-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002503_nop-emporium-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002504_nop-emporium-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002505_nop-emporium-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002506_nop-emporium-responsive-theme.jpeg"],
    accentColor: '#C0392B',
    overlayColor: 'rgba(192, 57, 43, 0.78)',
    desc: 'Çarpıcı görsellik ve güçlü kategori yönetimi. Büyük çok kategorili mağazalar için.'
  },
  {
    id: 'venture',
    name: 'Venture',
    subtitle: 'Minimal & Odaklı',
    tags: ["Minimal", "Odaklı", "Dönüşüm"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002472_nop-venture-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002472_nop-venture-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002473_nop-venture-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002474_nop-venture-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002475_nop-venture-responsive-theme.jpeg"],
    accentColor: '#27AE60',
    overlayColor: 'rgba(39, 174, 96, 0.75)',
    desc: 'Temiz ve minimal yaklaşım. Yüksek dönüşüm odaklı modern mağazalar için.'
  },
  {
    id: 'pavilion',
    name: 'Pavilion',
    subtitle: 'Lüks & Sofistike',
    tags: ["Lüks", "Sofistike", "Premium"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002348_nop-pavilion-responsive-theme.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002348_nop-pavilion-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002349_nop-pavilion-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002351_nop-pavilion-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002352_nop-pavilion-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002353_nop-pavilion-responsive-theme.png"],
    accentColor: '#2C2C54',
    overlayColor: 'rgba(44, 44, 84, 0.78)',
    desc: 'Sofistike tasarım ve lüks his. Premium ürün ve koleksiyon mağazaları için.'
  },
  {
    id: 'prisma',
    name: 'Prisma',
    subtitle: 'Renkli & Enerjik',
    tags: ["Renkli", "Enerjik", "Genç"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002464_nop-prisma-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002464_nop-prisma-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002465_nop-prisma-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002466_nop-prisma-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002467_nop-prisma-responsive-theme.jpeg"],
    accentColor: '#8E44AD',
    overlayColor: 'rgba(142, 68, 173, 0.75)',
    desc: 'Çarpıcı renkler ve dinamik düzen. Genç ve enerjik markalar için.'
  },
  {
    id: 'element',
    name: 'Element',
    subtitle: 'Sade & Etkili',
    tags: ["Sade", "Etkili", "Temiz"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002446_nop-element-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002446_nop-element-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002447_nop-element-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002448_nop-element-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0002449_nop-element-responsive-theme.jpeg"],
    accentColor: '#E67E22',
    overlayColor: 'rgba(230, 126, 34, 0.75)',
    desc: 'Sade ve etkili tasarım anlayışı. Her tür ürün için uygun esnek yapı.'
  },
  {
    id: 'uptown',
    name: 'Uptown',
    subtitle: 'Kentsel & Dinamik',
    tags: ["Kentsel", "Dinamik", "Moda"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002359_nop-uptown-responsive-theme.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002359_nop-uptown-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002360_nop-uptown-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002361_nop-uptown-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002362_nop-uptown-responsive-theme.png"],
    accentColor: '#1A1A2E',
    overlayColor: 'rgba(26, 26, 46, 0.82)',
    desc: 'Kentsel ve şık tasarım. Moda, sokak giyimi ve yaşam tarzı markaları için.'
  },
  {
    id: 'brooklyn',
    name: 'Brooklyn',
    subtitle: 'Hipster & Yaratıcı',
    tags: ["Yaratıcı", "Hipster", "Butik"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0001219_nop-brooklyn-responsive-theme.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0001219_nop-brooklyn-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001220_nop-brooklyn-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001221_nop-brooklyn-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001222_nop-brooklyn-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001223_nop-brooklyn-responsive-theme.png"],
    accentColor: '#4A235A',
    overlayColor: 'rgba(74, 35, 90, 0.78)',
    desc: 'Yaratıcı ve özgün tasarım. Butik, el yapımı ürünler ve sanatsal markalar için.'
  },
  {
    id: 'poppy',
    name: 'Poppy',
    subtitle: 'Feminen & Taze',
    tags: ["Feminen", "Taze", "Kozmetik"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002398_nop-poppy-responsive-theme.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002398_nop-poppy-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002399_nop-poppy-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0002400_nop-poppy-responsive-theme.png"],
    accentColor: '#C0392B',
    overlayColor: 'rgba(192, 57, 43, 0.75)',
    desc: 'Taze ve feminen tasarım. Kozmetik, güzellik ve kadın giyim markaları için.'
  },
  {
    id: 'minimal',
    name: 'Minimal',
    subtitle: 'Ultra Minimal & Saf',
    tags: ["Ultra Minimal", "Saf", "Temiz"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002376_nop-minimal-responsive-theme_430.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002376_nop-minimal-responsive-theme_430.png", "https://www.nop-templates.com/content/images/thumbs/0002377_nop-minimal-responsive-theme_430.png", "https://www.nop-templates.com/content/images/thumbs/0002378_nop-minimal-responsive-theme_430.png", "https://www.nop-templates.com/content/images/thumbs/0002379_nop-minimal-responsive-theme_430.png"],
    accentColor: '#2D3436',
    overlayColor: 'rgba(45, 52, 54, 0.78)',
    desc: 'Ürünü ön plana çıkaran ultra minimalist yapı. Premium tek ürün mağazaları için.'
  },
  {
    id: 'native',
    name: 'Native',
    subtitle: 'Doğal & Organik',
    tags: ["Doğal", "Organik", "Eko"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0001186_nop-native-responsive-theme.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0001186_nop-native-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001190_nop-native-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001191_nop-native-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001192_nop-native-responsive-theme.png"],
    accentColor: '#5D6D3E',
    overlayColor: 'rgba(93, 109, 62, 0.75)',
    desc: 'Doğal tonlar ve organik his. Gıda, bitki ve eko-dostu markalar için.'
  },
  {
    id: 'urban',
    name: 'Urban',
    subtitle: 'Şehirli & Modern',
    tags: ["Şehirli", "Modern", "Teknoloji"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0001181_nop-urban-responsive-theme.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0001181_nop-urban-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001182_nop-urban-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001183_nop-urban-responsive-theme.png", "https://www.nop-templates.com/content/images/thumbs/0001184_nop-urban-responsive-theme.png"],
    accentColor: '#2980B9',
    overlayColor: 'rgba(41, 128, 185, 0.78)',
    desc: 'Şehirli ve modern estetik. Teknoloji, elektronik ve erkek giyim markaları için.'
  },
  {
    id: 'alfresco',
    name: 'Alfresco',
    subtitle: 'Açık Hava & Ferah',
    tags: ["Açık Hava", "Ferah", "Spor"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000942_nop-alfresco-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000942_nop-alfresco-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000943_nop-alfresco-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000944_nop-alfresco-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000945_nop-alfresco-responsive-theme.jpeg"],
    accentColor: '#16A085',
    overlayColor: 'rgba(22, 160, 133, 0.75)',
    desc: 'Ferah ve enerjik tasarım. Spor, outdoor ve aktif yaşam markaları için.'
  },
  {
    id: 'alicante',
    name: 'Alicante',
    subtitle: 'Akdeniz & Sıcak',
    tags: ["Sıcak", "Akdeniz", "Lifestyle"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000934_nop-alicante-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000934_nop-alicante-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000935_nop-alicante-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000936_nop-alicante-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000937_nop-alicante-responsive-theme.jpeg"],
    accentColor: '#D35400',
    overlayColor: 'rgba(211, 84, 0, 0.75)',
    desc: 'Akdeniz esintisi ve sıcak renkler. Yiyecek, tatil ve lifestyle markaları için.'
  },
  {
    id: 'allure',
    name: 'Allure',
    subtitle: 'Çekici & Zarif',
    tags: ["Zarif", "Çekici", "Lüks"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000917_nop-allure-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000917_nop-allure-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000918_nop-allure-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000919_nop-allure-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000920_nop-allure-responsive-theme.jpeg"],
    accentColor: '#7D3C98',
    overlayColor: 'rgba(125, 60, 152, 0.78)',
    desc: 'Zarif ve çekici tasarım. Güzellik, kozmetik ve lüks aksesuar markaları için.'
  },
  {
    id: 'artfactory',
    name: 'ArtFactory',
    subtitle: 'Sanatsal & Yaratıcı',
    tags: ["Sanat", "Yaratıcı", "Galeri"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000913_nop-artfactory-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000913_nop-artfactory-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000914_nop-artfactory-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000915_nop-artfactory-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000916_nop-artfactory-responsive-theme.jpeg"],
    accentColor: '#1C2833',
    overlayColor: 'rgba(28, 40, 51, 0.82)',
    desc: 'Sanatsal ve yaratıcı sunum. Sanat galerisi, el sanatları ve kreatif markalar için.'
  },
  {
    id: 'lavella',
    name: 'Lavella',
    subtitle: 'Narin & Feminen',
    tags: ["Narin", "Feminen", "Butik"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000909_nop-lavella-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000909_nop-lavella-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000910_nop-lavella-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000911_nop-lavella-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000912_nop-lavella-responsive-theme.jpeg"],
    accentColor: '#A569BD',
    overlayColor: 'rgba(165, 105, 189, 0.75)',
    desc: 'Narin ve feminen estetik. Kadın giyim, takı ve butik mağazalar için.'
  },
  {
    id: 'lighthouse',
    name: 'Lighthouse',
    subtitle: 'Güvenilir & Sağlam',
    tags: ["Güvenilir", "Sağlam", "Kurumsal"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000938_nop-lighthouse-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000938_nop-lighthouse-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000939_nop-lighthouse-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000940_nop-lighthouse-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000941_nop-lighthouse-responsive-theme.jpeg"],
    accentColor: '#1F618D',
    overlayColor: 'rgba(31, 97, 141, 0.78)',
    desc: 'Güvenilir ve sağlam görünüm. Kurumsal ve B2B e-ticaret mağazaları için.'
  },
  {
    id: 'motion',
    name: 'Motion',
    subtitle: 'Dinamik & Hareketli',
    tags: ["Dinamik", "Hareketli", "Spor"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000921_nop-motion-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000921_nop-motion-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000922_nop-motion-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000923_nop-motion-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000924_nop-motion-responsive-theme.jpeg"],
    accentColor: '#E74C3C',
    overlayColor: 'rgba(231, 76, 60, 0.78)',
    desc: 'Dinamik ve enerjik tasarım. Spor ekipmanları ve aktif yaşam markaları için.'
  },
  {
    id: 'nitro',
    name: 'Nitro',
    subtitle: 'Hızlı & Agresif',
    tags: ["Hızlı", "Agresif", "Elektronik"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000905_nop-nitro-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000905_nop-nitro-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000906_nop-nitro-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000907_nop-nitro-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000908_nop-nitro-responsive-theme.jpeg"],
    accentColor: '#F39C12',
    overlayColor: 'rgba(243, 156, 18, 0.78)',
    desc: 'Güçlü ve agresif tasarım. Elektronik, oyun ve teknoloji markaları için.'
  },
  {
    id: 'smart',
    name: 'Smart',
    subtitle: 'Akıllı & Pratik',
    tags: ["Akıllı", "Pratik", "Çok Amaçlı"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000926_nop-smart-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000926_nop-smart-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000927_nop-smart-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000928_nop-smart-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000929_nop-smart-responsive-theme.jpeg"],
    accentColor: '#2ECC71',
    overlayColor: 'rgba(46, 204, 113, 0.75)',
    desc: 'Akıllı ve pratik çözüm. Her ölçekteki e-ticaret mağazası için.'
  },
  {
    id: 'traction',
    name: 'Traction',
    subtitle: 'Güçlü & Etkileyici',
    tags: ["Güçlü", "Etkileyici", "Dönüşüm"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0000900_nop-traction-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0000900_nop-traction-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000901_nop-traction-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000902_nop-traction-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0000904_nop-traction-responsive-theme.jpeg"],
    accentColor: '#34495E',
    overlayColor: 'rgba(52, 73, 94, 0.78)',
    desc: 'Güçlü ve etkileyici sunum. Yüksek dönüşüm hedefleyen mağazalar için.'
  },
  {
    id: 'tiffany',
    name: 'Tiffany',
    subtitle: 'Narin & Lüks',
    tags: ["Lüks", "Narin", "Takı"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0001119_nop-tiffany-responsive-theme.jpeg',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0001119_nop-tiffany-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0001120_nop-tiffany-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0001121_nop-tiffany-responsive-theme.jpeg", "https://www.nop-templates.com/content/images/thumbs/0001122_nop-tiffany-responsive-theme.jpeg"],
    accentColor: '#0ABFBF',
    overlayColor: 'rgba(10, 191, 191, 0.75)',
    desc: 'Zarif ve lüks his. Takı, mücevher ve premium aksesuar markaları için.'
  },
  {
    id: 'universe',
    name: 'Universe',
    subtitle: 'Kapsamlı & Evrensel',
    tags: ["Kapsamlı", "Evrensel", "Çok Amaçlı"],
    mainImage: 'https://www.nop-templates.com/content/images/thumbs/0002640_nop-universe-theme_600.png',
    gallery: ["https://www.nop-templates.com/content/images/thumbs/0002640_nop-universe-theme_600.png"],
    accentColor: '#2C3E50',
    overlayColor: 'rgba(44, 62, 80, 0.82)',
    desc: 'Her sektöre uygun kapsamlı yapı. Büyük çaplı evrensel e-ticaret projeleri için.'
  },
];

const IntegrationsStrip = () => {
  const items = ['Trendyol', 'Hepsiburada', 'n11', 'Amazon', 'Çiçeksepeti', 'iyzico', 'PayTR',
      'MNG Kargo', 'Yurtiçi Kargo', 'Aras Kargo', 'Sürat Kargo'];
  return (
    <div className="integrations-strip">
      <div className="container">
        <p className="integrations-label">Önde gelen platformlarla entegre</p>
      </div>
      <div className="integrations-overflow">
        <div className="integrations-track">
          {[...items, ...items].map((name, i) => (
            <span key={i} className="integration-pill">{name}</span>
          ))}
        </div>
      </div>
    </div>
  );
};

const FEATURE_TABS = [
  {
    icon: <BarChart3 size={22} />,
    title: 'Gerçek Zamanlı Analitik',
    desc: 'Satış, ziyaretçi ve dönüşüm verilerinizi anlık takip edin. Hangi ürün ne kadar sattı, en çok ziyaret edilen sayfaları görün.',
    color: '#2563EB',
    visual: (
      <div className="feature-visual-analytics">
        <div className="fv-stat">
          <span className="fv-stat-val">₺24.830</span>
          <span className="fv-stat-lbl">Bugünkü Satış <span className="fv-badge-up">↑ %18</span></span>
        </div>
        <div className="fv-bars">
          {[40, 65, 45, 80, 55, 90, 70].map((h, i) => (
            <div key={i} className="fv-bar-wrap">
              <motion.div
                className="fv-bar"
                initial={{ height: 0 }}
                animate={{ height: `${h}%` }}
                transition={{ delay: i * 0.06, duration: 0.5 }}
                style={{ background: i === 5 ? '#2563EB' : '#BFDBFE' }}
              />
              <span className="fv-bar-lbl">{['Pzt','Sal','Çar','Per','Cum','Cmt','Paz'][i]}</span>
            </div>
          ))}
        </div>
      </div>
    )
  },
  {
    icon: <Smartphone size={22} />,
    title: 'Mobil Önce Tasarım',
    desc: 'Tüm temalar mobil uyumlu. Müşterileriniz telefon, tablet veya bilgisayardan sorunsuz alışveriş yapabilir.',
    color: '#8B5CF6',
    visual: (
      <div className="feature-visual-mobile">
        <div className="fv-mobile-screen-wrap">
          <div className="fv-mobile-screen">
            <img
              src="https://www.nop-templates.com/content/images/thumbs/0002930_nop-pioneer-responsive-theme.jpeg"
              alt="Mağaza önizleme"
            />
          </div>
          <div className="fv-mobile-chip fv-chip-top">
            <Smartphone size={13} /> Mobil
          </div>
          <div className="fv-mobile-chip fv-chip-bottom">
            <Check size={13} /> %100 Uyumlu
          </div>
        </div>
      </div>
    )
  },
  {
    icon: <Globe size={22} />,
    title: 'Pazaryeri Entegrasyonu',
    desc: 'Trendyol, Hepsiburada ve n11\'e tek panelden ürün aktar. Stok ve sipariş yönetimini merkezi olarak yap.',
    color: '#F97316',
    visual: (
      <div className="feature-visual-marketplace">
        <div className="fv-mp-grid">
          {[
            { name: 'Trendyol', color: '#FF6000', emoji: '🛍️', orders: '142 sipariş' },
            { name: 'Hepsiburada', color: '#FF6D00', emoji: '🛒', orders: '89 sipariş' },
            { name: 'n11', color: '#7B2FF7', emoji: '🏪', orders: '56 sipariş' },
            { name: 'Amazon TR', color: '#FF9900', emoji: '📦', orders: '34 sipariş' },
          ].map(p => (
            <div key={p.name} className="fv-mp-card">
              <div className="fv-mp-card-top">
                <span className="fv-mp-emoji">{p.emoji}</span>
                <span className="fv-mp-name" style={{ color: p.color }}>{p.name}</span>
              </div>
              <div className="fv-mp-orders">{p.orders}</div>
              <div className="fv-mp-bar"><div className="fv-mp-bar-fill" style={{ background: p.color }} /></div>
            </div>
          ))}
        </div>
        <div className="fv-mp-total">
          <span>Toplam</span>
          <strong>321 aktif sipariş</strong>
        </div>
      </div>
    )
  },
  {
    icon: <ShieldCheck size={22} />,
    title: 'Güvenli Ödeme',
    desc: 'iyzico, PayTR ve tüm sanal POS\'larla entegre. 3D Secure ve taksit seçenekleriyle müşteri güveni kazanın.',
    color: '#10B981',
    visual: (
      <div className="feature-visual-payment">
        <div className="fv-card">
          <div className="fv-card-chip" />
          <div className="fv-card-num">•••• •••• •••• 4242</div>
          <div className="fv-card-bottom">
            <span>Müşteri Adı</span>
            <span>12/27</span>
          </div>
        </div>
        <div className="fv-payment-ok">
          <CheckCircle2 size={20} />
          <span>Ödeme Onaylandı</span>
        </div>
      </div>
    )
  }
];

const AnimatedFeaturesSection = () => {
  const [active, setActive] = useState(0);
  return (
    <section className="animated-features section-padding">
      <div className="container">
        <div className="section-header">
          <span className="text-gradient">Platform</span>
          <h2>Satışı Artıran Özellikler</h2>
          <p className="section-desc">İhtiyacınız olan her araç, tek bir panelde.</p>
        </div>
        <div className="af-layout">
          <div className="af-tabs">
            {FEATURE_TABS.map((tab, i) => (
              <button
                key={i}
                className={`af-tab ${active === i ? 'active' : ''}`}
                onClick={() => setActive(i)}
                style={active === i ? { borderColor: tab.color } : {}}
              >
                <span className="af-tab-icon" style={{ color: active === i ? tab.color : '#94A3B8' }}>{tab.icon}</span>
                <div>
                  <div className="af-tab-title" style={{ color: active === i ? tab.color : '#0F172A' }}>{tab.title}</div>
                  {active === i && <div className="af-tab-desc">{tab.desc}</div>}
                </div>
              </button>
            ))}
          </div>
          <div className="af-visual-panel" style={{ background: `linear-gradient(135deg, ${FEATURE_TABS[active].color}12, ${FEATURE_TABS[active].color}04)`, borderColor: `${FEATURE_TABS[active].color}30` }}>
            <AnimatePresence mode="wait">
              <motion.div
                key={active}
                initial={{ opacity: 0, y: 16 }}
                animate={{ opacity: 1, y: 0 }}
                exit={{ opacity: 0, y: -16 }}
                transition={{ duration: 0.22 }}
                className="af-visual-inner"
              >
                {FEATURE_TABS[active].visual}
              </motion.div>
            </AnimatePresence>
          </div>
        </div>
      </div>
    </section>
  );
};

const EcommerceTeaser = ({ onGoEcommerce }) => (
  <section className="ecommerce-teaser section-padding bg-soft">
    <div className="container">
      <div className="section-header">
        <span className="text-gradient">Temalar</span>
        <h2>Premium Temalar</h2>
        <p className="section-desc">Her sektöre uygun, profesyonel e-ticaret tasarımları. Hepsine sahipsiniz.</p>
      </div>
      <div className="teaser-themes-grid">
        {THEMES_DATA.slice(0, 3).map(theme => (
          <div key={theme.id} className="teaser-theme-card" onClick={onGoEcommerce}>
            <img src={theme.mainImage} alt={theme.name} />
            <div className="teaser-theme-info">
              <span className="teaser-theme-name">{theme.name}</span>
              <span className="teaser-theme-sub">{theme.subtitle}</span>
            </div>
          </div>
        ))}
      </div>
      <div className="teaser-cta">
        <button className="btn-primary" onClick={onGoEcommerce}>
          Tüm Temaları ve Paketleri Gör <ArrowRight size={18} />
        </button>
      </div>
    </div>
  </section>
);

const BLOG_POSTS = [
  {
    slug: 'eticarette-seo',
    tag: 'SEO', color: '#2563EB',
    title: 'E-Ticarette SEO: Mağazanızı Google\'da Öne Çıkarın',
    desc: 'Doğru URL yapısı, meta etiketler ve içerik stratejisiyle organik trafiğinizi katlayın.',
    readTime: '5 dk',
    img: 'https://images.unsplash.com/photo-1432888498266-38ffec3eaf0a?w=600&q=80',
    content: [
      { type: 'p', text: 'E-ticaret mağazanızın Google\'da üst sıralarda yer alması, reklam bütçesi harcamadan sürdürülebilir bir müşteri kitlesi oluşturmanın en etkili yoludur. Ancak e-ticaret SEO\'su, blog veya kurumsal site optimizasyonundan farklı dinamiklere sahiptir.' },
      { type: 'h2', text: '1. URL Yapısını Doğru Kur' },
      { type: 'p', text: 'Ürün ve kategori URL\'leriniz hem arama motorları hem de kullanıcılar için anlaşılır olmalıdır. Örneğin "/urun?id=4521" yerine "/erkek-ayakkabi/spor-kosu-ayakkabisi" formatı hem SEO\'ya hem dönüşüm oranına olumlu katkı sağlar. Gereksiz parametre ve session ID\'lerden kaçının.' },
      { type: 'h2', text: '2. Meta Etiketleri Optimize Edin' },
      { type: 'p', text: 'Her ürün sayfasının benzersiz bir title ve meta description\'a sahip olması gerekir. Title etiketi 55–60 karakter arasında tutulmalı, hedef anahtar kelimeyi içermeli ve marka adıyla bitirilmelidir. Meta description ise 150–160 karakter ile kullanıcıyı tıklamaya teşvik eden bir çağrı içermelidir.' },
      { type: 'h2', text: '3. Ürün Açıklamalarını Özgün Yazın' },
      { type: 'p', text: 'Tedarikçiden kopyalanan ürün açıklamaları Google tarafından yinelenen içerik olarak değerlendirilebilir ve sıralamalarınızı olumsuz etkiler. Her ürün için özgün, kullanıcının sorularını yanıtlayan ve hedef anahtar kelimeleri doğal biçimde barındıran açıklamalar yazın.' },
      { type: 'h2', text: '4. Sayfa Hızına Önem Verin' },
      { type: 'p', text: 'Google, Core Web Vitals metriklerini sıralama faktörü olarak kullanmaktadır. Görsel boyutlarını optimize edin, gereksiz JavaScript yüklemelerini erteleyin ve bir CDN kullanın. Hedef, LCP (Largest Contentful Paint) değerinin 2,5 saniyenin altında kalmasıdır.' },
      { type: 'h2', text: '5. İç Linkleme Stratejisi Kurun' },
      { type: 'p', text: 'Kategori sayfalarınızdan ürün sayfalarına, blog yazılarınızdan ilgili ürünlere doğal bağlantılar ekleyin. Bu sayede tarama bütçenizi verimli kullanır, link otoritesini mağazanız genelinde dengeli dağıtır ve kullanıcıların sitede daha uzun vakit geçirmesini sağlarsınız.' },
      { type: 'p', text: 'Pekin Teknoloji altyapısıyla açılan mağazalar, SEO dostu URL yapısı, otomatik sitemap oluşturma ve sayfa hızı optimizasyonları ile bu adımların büyük bölümünü kutudan çıkar çıkmaz karşılamaktadır.' },
    ],
  },
  {
    slug: 'trendyol-entegrasyonu',
    tag: 'Pazaryeri', color: '#F97316',
    title: 'Trendyol Entegrasyonu ile Satışlarınızı Artırın',
    desc: 'Ürünlerinizi Trendyol ile senkronize edin, stok ve siparişleri tek panelden yönetin.',
    readTime: '4 dk',
    img: 'https://images.unsplash.com/photo-1556742049-0cfed4f6a45d?w=600&q=80',
    content: [
      { type: 'p', text: '30 milyonu aşkın aktif alıcısıyla Trendyol, e-ticaret satıcıları için vazgeçilmez bir satış kanalıdır. Ancak mağazanız ile Trendyol\'u ayrı ayrı yönetmek ciddi zaman ve hata kaybına yol açabilir. İşte bu noktada entegrasyon devreye girer.' },
      { type: 'h2', text: 'Entegrasyon Ne İşe Yarar?' },
      { type: 'p', text: 'Trendyol entegrasyonu sayesinde ürün bilgileri, fiyatlar ve stok miktarları mağazanızdan otomatik olarak Trendyol\'a aktarılır. Trendyol\'dan gelen siparişler anında kendi panelinizde görünür; iki farklı ekranda takip etme derdiniz ortadan kalkar.' },
      { type: 'h2', text: 'Stok Senkronizasyonu' },
      { type: 'p', text: 'Aynı ürünü hem kendi mağazanızda hem Trendyol\'da satıyorsanız stok senkronizasyonu kritiktir. Entegrasyon olmadan bir kanalda satış gerçekleştiğinde diğer kanalda stok güncellenmez; bu da fazla satış (oversell) ve müşteri memnuniyetsizliğine yol açar. Gerçek zamanlı senkronizasyon bu riski tamamen ortadan kaldırır.' },
      { type: 'h2', text: 'Sipariş Yönetimi' },
      { type: 'p', text: 'Trendyol siparişleri kendi panelinizde konsolide edildiğinde kargo etiketleri, fatura ve müşteri bildirimleri tek yerden yönetilir. İade ve iptal süreçleri de entegrasyon üzerinden otomatik olarak Trendyol sistemine yansıtılır.' },
      { type: 'h2', text: 'Fiyat Yönetimi' },
      { type: 'p', text: 'Trendyol\'da farklı fiyatlandırma politikası uygulamak isteyebilirsiniz. İyi bir entegrasyon, her kanal için ayrı fiyat kuralları tanımlamanıza olanak tanır; kampanya dönemlerinde tek tıkla toplu fiyat güncellemesi yapabilirsiniz.' },
      { type: 'p', text: 'Pekin Teknoloji mağazaları, Trendyol başta olmak üzere Hepsiburada ve N11 entegrasyonlarını desteklemektedir. Tek panelden tüm kanallarınızı yönetmeye hemen başlayabilirsiniz.' },
    ],
  },
  {
    slug: 'dogru-tema-secimi',
    tag: 'Tasarım', color: '#8B5CF6',
    title: 'E-Ticaret Teması Seçerken Dikkat Edilmesi Gerekenler',
    desc: 'Dönüşüm oranını etkileyen tasarım kararları ve doğru tema seçim kriterleri.',
    readTime: '6 dk',
    img: 'https://images.unsplash.com/photo-1467232004584-a241de8bcf5d?w=600&q=80',
    content: [
      { type: 'p', text: 'E-ticaret mağazanızın görsel kimliği, ziyaretçilerin satın alma kararı üzerinde doğrudan etkilidir. Araştırmalar, kullanıcıların bir web sitesine dair ilk yargıyı milisaniyeler içinde oluşturduğunu ortaya koymaktadır. Bu nedenle doğru temayı seçmek, dönüşüm oranınızı belirleyen en kritik kararlardan biridir.' },
      { type: 'h2', text: '1. Mobil Uyumluluk Önce Gelir' },
      { type: 'p', text: 'E-ticaret trafiğinin büyük bölümü mobil cihazlardan gelmektedir. Seçeceğiniz temanın tüm ekran boyutlarında kusursuz çalıştığını, mobil menünün kullanışlı olduğunu ve ödeme adımlarının parmakla rahatlıkla tamamlanabildiğini test edin.' },
      { type: 'h2', text: '2. Sayfa Yükleme Hızı' },
      { type: 'p', text: 'Görsel olarak etkileyici ama yavaş yüklenen temalar dönüşüm oranını ciddi biçimde düşürür. Her 1 saniyelik gecikme, dönüşümde yaklaşık %7 kayba yol açtığı bilinmektedir. Tema seçerken demo sitenin Google PageSpeed Insights skorunu mutlaka kontrol edin; mobil skoru 70\'in üzerinde olmalıdır.' },
      { type: 'h2', text: '3. Ürün Sergisi ve Görsel Alanlar' },
      { type: 'p', text: 'Ürün görselleri e-ticarette satışın en güçlü aracıdır. Temanın büyük ve yüksek çözünürlüklü görsel desteği sunduğundan, zoom özelliği ve çoklu görsel galeri imkânı sağladığından emin olun. Moda ve güzellik kategorileri için tam ekran görsel alanları tercih edilmelidir.' },
      { type: 'h2', text: '4. Sepet ve Ödeme Akışı' },
      { type: 'p', text: 'Karmaşık ya da çok adımlı bir ödeme süreci sepet terk oranını artırır. Mini sepet, tek sayfa ödeme (one-page checkout) ve misafir alışveriş desteği sunan temalar dönüşüm oranını belirgin biçimde yükseltir. Bu özelliklerin tema demo\'sunda çalışıp çalışmadığını bizzat deneyin.' },
      { type: 'h2', text: '5. Özelleştirme Kolaylığı' },
      { type: 'p', text: 'Marka kimliğinize uygun renk paleti, tipografi ve banner düzenlemelerini kod bilgisi gerektirmeden yapabilmelisiniz. Drag-and-drop sayfa düzenleyici desteği olan temalar, pazarlama kampanyalarınızda çok daha hızlı hareket etmenizi sağlar.' },
      { type: 'p', text: 'Pekin Teknoloji, her sektöre uygun premium temalar sunmaktadır. Tüm temalar mobil uyumlu, hızlı yüklemeli ve kapsamlı özelleştirme seçenekleriyle donatılmıştır. Demo mağazalarımızı inceleyerek mağazanız için en uygun tasarımı seçebilirsiniz.' },
    ],
  },
];

const BlogSection = ({ onNavigate }) => (
  <section className="blog-section section-padding">
    <div className="container">
      <div className="section-header">
        <span className="text-gradient">Blog</span>
        <h2>E-Ticaret Rehberi</h2>
        <p className="section-desc">Mağazanızı büyütmek için ipuçları ve stratejiler.</p>
      </div>
      <div className="blog-grid">
        {BLOG_POSTS.map((post, i) => (
          <div key={i} className="blog-card" onClick={() => onNavigate && onNavigate('blog', post.slug)} style={{ cursor: 'pointer' }}>
            <div className="blog-card-img">
              <img src={post.img} alt={post.title} />
              <span className="blog-tag" style={{ background: post.color }}>{post.tag}</span>
            </div>
            <div className="blog-card-body">
              <h3>{post.title}</h3>
              <p>{post.desc}</p>
              <div className="blog-card-footer">
                <span className="blog-read-time">{post.readTime} okuma</span>
                <span className="blog-read-more" style={{ color: post.color }}>Devamını Oku →</span>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  </section>
);

const FAQ_ITEMS = [
  { q: 'Mağaza açmak için teknik bilgi gerekiyor mu?', a: 'Hayır. Kurulum ve teknik ayarları biz yapıyoruz. Siz sadece ürünlerinizi yükleyin ve satmaya başlayın.' },
  { q: 'Hangi ödeme sistemleri destekleniyor?', a: 'iyzico, PayTR, Garanti BBVA Sanal POS, Yapı Kredi, Akbank ve tüm sanal POS sağlayıcılarıyla entegrasyon desteği sunuyoruz.' },
  { q: 'Trendyol ve Hepsiburada entegrasyonu nasıl çalışıyor?', a: 'Ürünlerinizi tek bir panelden tüm pazaryerlerine aktarın, sipariş ve stok yönetimini merkezi olarak yapın.' },
  { q: 'Kaç farklı tema var, özelleştirebilir miyim?', a: 'Premium temalar arasından seçim yapabilirsiniz. Renk, font ve layout tamamen markanıza göre özelleştirilebilir.' },
  { q: 'Teknik destek ne zaman ulaşılabilir?', a: '7/24 teknik destek ekibimiz her an yanınızda. Acil durumlarda maksimum 2 saat içinde müdahale garantisi veriyoruz.' },
  { q: 'Mevcut mağazamı taşıyabilir miyim?', a: 'Evet. Ürünlerinizi, müşteri verilerinizi ve sipariş geçmişinizi mevcut altyapınızdan aktarıyoruz.' },
];

const FAQSection = () => {
  const [open, setOpen] = useState(null);
  return (
    <section className="faq-section section-padding bg-soft">
      <div className="container">
        <div className="section-header">
          <span className="text-gradient">SSS</span>
          <h2>Sıkça Sorulan Sorular</h2>
        </div>
        <div className="faq-list">
          {FAQ_ITEMS.map((item, i) => (
            <div key={i} className={`faq-item ${open === i ? 'open' : ''}`}>
              <button className="faq-question" onClick={() => setOpen(open === i ? null : i)}>
                <span>{item.q}</span>
                <ChevronDown size={20} className="faq-chevron" />
              </button>
              <div className="faq-answer">
                <p>{item.a}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

const NAV_DROPDOWN = {
  cols: [
    {
      heading: 'Yazılım & Dijital',
      items: [
        { icon: '🌐', label: 'Kurumsal Web Sitesi', desc: 'Hızlı, SEO uyumlu kurumsal siteler' },
        { icon: '📱', label: 'Mobil Uygulama', desc: 'iOS & Android uygulamaları' },
        { icon: '💻', label: 'Özel Yazılım', desc: 'ERP, CRM ve iş akışı çözümleri' },
        { icon: '🎯', label: 'Teknoloji Danışmanlığı', desc: 'Dijital dönüşüm planlaması' },
      ]
    },
    {
      heading: 'E-Ticaret',
      items: [
        { icon: '🚀', label: 'Mağaza Kurulum', desc: 'Domain, SSL, ödeme ve kargo dahil' },
        { icon: '🎨', label: 'Tema Özelleştirme', desc: 'Premium temalar, markanıza göre' },
        { icon: '🔗', label: 'Pazaryeri Entegrasyonu', desc: 'Trendyol, Hepsiburada, n11, Amazon' },
        { icon: '🛠️', label: 'Teknik Destek & Bakım', desc: '7/24 destek ekibi' },
      ]
    }
  ]
};

const Navbar = ({ onConsult }) => {
  const navigate = useNavigate();
  const location = useLocation();
  const [isScrolled, setIsScrolled] = useState(false);
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const [mobileOpen, setMobileOpen] = useState(false);
  const dropdownRef = React.useRef(null);

  useEffect(() => {
    const handleScroll = () => setIsScrolled(window.scrollY > 20);
    const handleClick = (e) => {
      if (dropdownRef.current && !dropdownRef.current.contains(e.target)) setDropdownOpen(false);
    };
    window.addEventListener('scroll', handleScroll);
    document.addEventListener('mousedown', handleClick);
    return () => {
      window.removeEventListener('scroll', handleScroll);
      document.removeEventListener('mousedown', handleClick);
    };
  }, []);

  useEffect(() => {
    document.body.style.overflow = mobileOpen ? 'hidden' : '';
    return () => { document.body.style.overflow = ''; };
  }, [mobileOpen]);

  const goSection = (id) => {
    setDropdownOpen(false);
    setMobileOpen(false);
    if (location.pathname !== '/') {
      navigate('/');
      setTimeout(() => document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' }), 200);
    } else {
      document.getElementById(id)?.scrollIntoView({ behavior: 'smooth' });
    }
  };

  return (
    <>
      {dropdownOpen && <div className="nav-backdrop" onClick={() => setDropdownOpen(false)} />}
      {mobileOpen && <div className="nav-mobile-backdrop" onClick={() => setMobileOpen(false)} />}
      <nav className={`navbar ${isScrolled ? 'scrolled' : ''}`}>
        <div className="nav-container">
          <div className="logo" onClick={() => { setDropdownOpen(false); setMobileOpen(false); navigate('/'); }}>
            <Logo size="md" />
          </div>

          {/* Desktop menü */}
          <div className="nav-links">
            <div className="nav-dropdown-wrap" ref={dropdownRef}>
              <button
                className={`nav-dropdown-trigger ${dropdownOpen ? 'active' : ''}`}
                onClick={() => setDropdownOpen(v => !v)}
              >
                Hizmetler <ChevronDown size={15} className={`nav-trigger-chevron ${dropdownOpen ? 'open' : ''}`} />
              </button>
              <AnimatePresence>
                {dropdownOpen && (
                  <motion.div
                    className="nav-dropdown"
                    initial={{ opacity: 0, y: 8 }}
                    animate={{ opacity: 1, y: 0 }}
                    exit={{ opacity: 0, y: 8 }}
                    transition={{ duration: 0.18 }}
                  >
                    {NAV_DROPDOWN.cols.map(col => (
                      <div key={col.heading} className="nav-dropdown-col">
                        <span className="nav-dropdown-heading">{col.heading}</span>
                        {col.items.map(item => (
                          <button key={item.label} className="nav-dropdown-item" onClick={() => goSection('services')}>
                            <span className="nav-item-icon">{item.icon}</span>
                            <div>
                              <div className="nav-item-label">{item.label}</div>
                              <div className="nav-item-desc">{item.desc}</div>
                            </div>
                          </button>
                        ))}
                      </div>
                    ))}
                  </motion.div>
                )}
              </AnimatePresence>
            </div>

            <Link to="/eticaret" onClick={() => setDropdownOpen(false)}>E-Ticaret</Link>
            <a href="#" onClick={(e) => { e.preventDefault(); goSection('contact'); }}>İletişim</a>

            <div className="nav-cta-group">
              <button className="btn-nav" onClick={() => { setDropdownOpen(false); onConsult(); }}>Ücretsiz Danışmanlık Al</button>
            </div>
          </div>

          {/* Hamburger butonu */}
          <button className="nav-hamburger" onClick={() => setMobileOpen(v => !v)} aria-label="Menü">
            {mobileOpen ? <X size={24} /> : <Menu size={24} />}
          </button>
        </div>

        {/* Mobile menü */}
        <AnimatePresence>
          {mobileOpen && (
            <motion.div
              className="nav-mobile-menu"
              initial={{ opacity: 0, y: -10 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0, y: -10 }}
              transition={{ duration: 0.2 }}
            >
              <button className="nav-mobile-item" onClick={() => goSection('services')}>Hizmetler</button>
              <Link className="nav-mobile-item" to="/eticaret" onClick={() => setMobileOpen(false)}>E-Ticaret</Link>
              <button className="nav-mobile-item" onClick={() => goSection('contact')}>İletişim</button>
              <button className="btn-nav nav-mobile-cta" onClick={() => { setMobileOpen(false); onConsult(); }}>Ücretsiz Danışmanlık Al</button>
            </motion.div>
          )}
        </AnimatePresence>
      </nav>
    </>
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
            {theme.gallery.map((img, idx) => {
              const labels4 = ['Ana Sayfa', 'Kategori', 'Ürün Detay', 'Ödeme'];
              const labels5 = ['Ana Sayfa', 'Kategori', 'Ürün Detay', 'Ürün Detay', 'Ödeme'];
              const labels3 = ['Ana Sayfa', 'Kategori', 'Ürün Detay'];
              const total = theme.gallery.length;
              const label = total >= 5 ? (labels5[idx] || 'Diğer') : total === 4 ? (labels4[idx] || 'Diğer') : (labels3[idx] || 'Diğer');
              return (
                <div key={idx} className="gallery-item">
                  <div className="gallery-label">{label}</div>
                  <img src={img} alt={`${theme.name} screenshot ${idx + 1}`} />
                </div>
              );
            })}
          </div>

          <div className="modal-footer">
            <button className="btn-primary" onClick={() => {
              onClose();
              window.dispatchEvent(new CustomEvent('open-registration'));
            }}>
              Bu Temayı Denemek İçin Talep Oluşturabilirsiniz!
            </button>
          </div>
        </motion.div>
      </motion.div>
    </AnimatePresence>
  );
};

const ContactInlineForm = () => {
  const [formData, setFormData] = useState({ name: '', email: '', phone: '', message: '' });
  const [sent, setSent] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      await fetch('https://test.pekinteknoloji.com/api/fastregister/consultation', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ ...formData, subject: 'İletişim Formu' }),
      });
      setSent(true);
    } catch {
      setSent(true);
    } finally {
      setLoading(false);
    }
  };

  const set = (field) => (e) => setFormData({ ...formData, [field]: e.target.value });

  return (
    <div className="contact-form-center">
      {sent ? (
        <div className="contact-form-success">
          <CheckCircle2 size={44} className="success-icon" />
          <h3>Mesajınız Alındı!</h3>
          <p>En geç 1 iş günü içinde size dönüş yapıyoruz.</p>
        </div>
      ) : (
        <form onSubmit={handleSubmit} className="contact-inline-form">
          <div className="cf-row">
            <div className="cf-group">
              <label>Ad Soyad</label>
              <input type="text" required placeholder="Adınız Soyadınız" value={formData.name} onChange={set('name')} />
            </div>
            <div className="cf-group">
              <label>Telefon</label>
              <input type="tel" placeholder="05XX XXX XX XX" value={formData.phone} onChange={e => setFormData({ ...formData, phone: formatPhone(e.target.value) })} inputMode="numeric" pattern="[0-9 ]{13,14}" />
            </div>
            <div className="cf-group">
              <label>E-posta</label>
              <input type="email" required placeholder="ornek@mail.com" value={formData.email} onChange={set('email')} />
            </div>
            <div className="cf-group">
              <label>Mesaj</label>
              <textarea required rows={3} placeholder="Projeniz hakkında bilgi verin..." value={formData.message} onChange={set('message')} />
            </div>
          </div>
          <button type="submit" className="cf-submit" disabled={loading}>
            {loading ? 'Gönderiliyor...' : 'Mesaj Gönder'} {!loading && <ArrowRight size={16} />}
          </button>
        </form>
      )}
    </div>
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
      const res = await fetch('https://test.pekinteknoloji.com/api/fastregister/consultation', {
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
                  <input type="tel" required placeholder="05XX XXX XX XX" value={formData.phone} onChange={e => setFormData({...formData, phone: formatPhone(e.target.value)})} inputMode="numeric" pattern="[0-9 ]{13,14}" />
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

const formatPhone = (value) => {
  const digits = value.replace(/\D/g, '').slice(0, 11);
  if (digits.length <= 4) return digits;
  if (digits.length <= 7) return `${digits.slice(0, 4)} ${digits.slice(4)}`;
  if (digits.length <= 9) return `${digits.slice(0, 4)} ${digits.slice(4, 7)} ${digits.slice(7)}`;
  return `${digits.slice(0, 4)} ${digits.slice(4, 7)} ${digits.slice(7, 9)} ${digits.slice(9)}`;
};

const toSlug = (text) =>
  text.toLocaleLowerCase('tr-TR')
    .replace(/ğ/g, 'g').replace(/ü/g, 'u').replace(/ş/g, 's')
    .replace(/ı/g, 'i').replace(/ö/g, 'o').replace(/ç/g, 'c')
    .replace(/[^a-z0-9\s-]/g, '')
    .trim().replace(/\s+/g, '-');

const useSlugCheck = () => {
  const [slugStatus, setSlugStatus] = useState(null); // null | 'checking' | 'available' | 'taken'
  const timerRef = React.useRef(null);

  const checkSlug = (slug) => {
    if (!slug) { setSlugStatus(null); return; }
    setSlugStatus('checking');
    clearTimeout(timerRef.current);
    timerRef.current = setTimeout(async () => {
      try {
        const res = await fetch(`https://test.pekinteknoloji.com/api/fastregister/check-slug?slug=${encodeURIComponent(slug)}`);
        const data = await res.json();
        setSlugStatus(data.available ? 'available' : 'taken');
      } catch {
        setSlugStatus(null);
      }
    }, 500);
  };

  return { slugStatus, checkSlug };
};

const RegistrationModal = ({ isOpen, onClose }) => {
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    storeName: '',
    storeSlug: '',
    email: '',
    phone: '',
    password: ''
  });
  const [loading, setLoading] = useState(false);
  const { slugStatus, checkSlug } = useSlugCheck();

  const handleStoreNameChange = (e) => {
    const name = e.target.value;
    const slug = toSlug(name);
    setFormData({ ...formData, storeName: name, storeSlug: slug });
    checkSlug(slug);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    try {
      const response = await fetch("https://test.pekinteknoloji.com/api/fastregister/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });

      const data = await response.json();

      if (data.success) {
        window.location.href = data.autoLoginUrl;
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
            <div className="form-group">
              <label>Mağaza Adı</label>
              <input type="text" required placeholder="Mağazanızın adı" value={formData.storeName} onChange={handleStoreNameChange} />
              {formData.storeSlug && (
                <small style={{ display: 'block', marginTop: 4, color: slugStatus === 'available' ? '#16a34a' : slugStatus === 'taken' ? '#dc2626' : '#6b7280' }}>
                  {slugStatus === 'checking' && `🔍 ${formData.storeSlug}.pekinteknoloji.com kontrol ediliyor...`}
                  {slugStatus === 'available' && `✅ ${formData.storeSlug}.pekinteknoloji.com müsait`}
                  {slugStatus === 'taken' && `❌ ${formData.storeSlug}.pekinteknoloji.com kullanılıyor`}
                  {slugStatus === null && `🌐 ${formData.storeSlug}.pekinteknoloji.com`}
                </small>
              )}
            </div>
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
              <input type="tel" required placeholder="05XX XXX XX XX" value={formData.phone} onChange={e => setFormData({...formData, phone: formatPhone(e.target.value)})} inputMode="numeric" pattern="[0-9 ]{13,14}" />
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
      { text: 'Premium temalar', ok: true },
      { text: 'Gelişmiş SEO & blog modülü', ok: true },
      { text: 'Tüm ödeme sistemleri', ok: true },
      { text: '3 kargo entegrasyonu', ok: true },
      { text: 'Manuel sipariş oluşturma', ok: true },
      { text: '7/24 telefon & e-posta desteği', ok: true },
      { text: 'Terk edilmiş sepet otomasyonu', ok: true },
      { text: 'Trendyol & Hepsiburada entegrasyonu', ok: true },
      { text: 'Kampanya & kupon yönetimi', ok: true },
      { text: 'Toplu ürün yönetimi', ok: true },
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
    ],
  },
];

const EC_FEATURES = [
  { icon: <Zap size={22}/>, title: 'Hızlı Yükleme', desc: 'Optimize edilmiş altyapı ve CDN desteğiyle sayfalarınız hızlı açılır, kullanıcı deneyimi ve dönüşüm oranı artar.' },
  { icon: <TrendingUp size={22}/>, title: 'Gelişmiş SEO', desc: 'SEO dostu URL yapısı, otomatik sitemap, meta etiket yönetimi ve yapısal veri desteğiyle arama motorlarında öne çıkın.' },
  { icon: <Cpu size={22}/>, title: 'Pazaryeri Entegrasyonları', desc: 'Trendyol, Hepsiburada ve n11 ile ürün, stok ve sipariş senkronizasyonu tek panelden.' },
  { icon: <MousePointer2 size={22}/>, title: 'Toplu Ürün Yönetimi', desc: 'Binlerce ürünü Excel ile içe aktarın, toplu fiyat ve stok güncellemelerini saniyeler içinde yapın.' },
  { icon: <Smartphone size={22}/>, title: 'Responsive Tasarım', desc: 'Tüm temalar mobil uyumlu; telefon, tablet ve masaüstünde kusursuz görünür.' },
  { icon: <Headphones size={22}/>, title: '7/24 Destek', desc: 'Teknik sorunlarınız için destek ekibimiz her zaman yanınızda.' },
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
          <span className="ec-badge"><span className="ec-badge-dot" />Güçlü E-Ticaret Altyapısı</span>
          <h1 className="ec-hero-title">E-ticarette<br /><span className="text-gradient">güçlü altyapı,</span><br />tam entegrasyon</h1>
          <p className="ec-hero-sub">E-ticaret sitenizi kurun, tüm pazaryerlerine bağlayın. Sipariş, stok ve kargo yönetimi tek panelden. 14 gün ücretsiz deneyin.</p>
          <div className="ec-hero-btns">
            <button className="btn-primary" onClick={onRegister}>Ücretsiz 14 Gün Deneyin <ArrowRight size={18} /></button>
            <button className="btn-ghost" onClick={() => document.getElementById('packages')?.scrollIntoView({ behavior: 'smooth' })}>Paketleri İncele</button>
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
          <h2>Markanıza özel premium temalar</h2>
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
              Diğer {THEMES_DATA.length - 3} Temayı Keşfet <ChevronRight size={16} />
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
    { title: 'Responsive Tasarım', desc: 'Tüm temalar mobil uyumlu; telefon, tablet ve masaüstünde kusursuz çalışır.', icon: <Smartphone /> },
    { title: '7/24 Destek', desc: 'Teknik sorunlarınız için destek ekibimiz her zaman yanınızda.', icon: <Headphones /> }
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
          Paketleri ve Temaları İncele <ArrowRight size={18} />
        </button>
      </div>
    </div>
  );
};

const PricingPage = () => {
  const [isYearly, setIsYearly] = useState(false);

  const plans = [
    { name: 'Başlangıç', monthly: '1.490', yearly: '14.900', enterprise: false },
    { name: 'Profesyonel', monthly: '2.990', yearly: '29.900', popular: true, enterprise: false },
    { name: 'Kurumsal', monthly: '7.490', yearly: '74.900', enterprise: true }
  ];

  const features = [
    { cat: 'Genel Özellikler' },
    { name: 'Ürün Limiti', values: ['500', '10.000', 'Sınırsız'] },
    { name: 'Depolama Alanı', values: ['5 GB', '25 GB', 'Sınırsız'] },
    { name: 'Hazır Tasarımlar', values: [true, true, true] },
    { cat: 'Pazarlama & SEO' },
    { name: 'Gelişmiş SEO', values: [true, true, true] },
    { name: 'Pazaryeri Entegrasyonları', values: [false, true, true] },
    { cat: 'Destek' },
    { name: '7/24 Destek', values: [true, true, true] },
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

          {/* Billing toggle */}
          <div className="pricing-toggle-wrap">
            <span className={!isYearly ? 'toggle-label active' : 'toggle-label'}>Aylık</span>
            <button
              className={`pricing-toggle ${isYearly ? 'on' : ''}`}
              onClick={() => setIsYearly(v => !v)}
              aria-label="Yıllık/Aylık fiyat değiştir"
            >
              <span className="pricing-toggle-knob" />
            </button>
            <span className={isYearly ? 'toggle-label active' : 'toggle-label'}>
              Yıllık <span className="toggle-saving">2 ay ücretsiz</span>
            </span>
          </div>

          <div className="pricing-grid mb-20">
            {plans.map(p => (
              <div key={p.name} className={`pricing-card ${p.popular ? 'popular' : ''}`}>
                {p.popular && <div className="popular-badge">En Popüler</div>}
                <h3>{p.name}</h3>
                <div className="price">
                  ₺{isYearly ? p.yearly : p.monthly}
                  <span>/{isYearly ? 'yıl' : 'ay'}</span>
                </div>
                {isYearly && (
                  <div className="price-monthly-note">
                    aylık ₺{p.monthly} yerine
                  </div>
                )}
                {p.enterprise ? (
                  <button className="btn-outline btn-full" onClick={() => window.dispatchEvent(new CustomEvent('open-consult'))}>
                    Satış Ekibiyle Konuş
                  </button>
                ) : (
                  <button className="btn-primary btn-full" onClick={() => window.dispatchEvent(new CustomEvent('open-registration'))}>
                    Hemen Başla
                  </button>
                )}
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

const HesabimPage = () => {
  const [data, setData] = React.useState(null);
  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState(null);

  React.useEffect(() => {
    fetch('https://test.pekinteknoloji.com/api/fastregister/my-store', { credentials: 'include' })
      .then(r => r.json())
      .then(d => { setData(d); setLoading(false); })
      .catch(() => { setError('Sunucuya bağlanılamadı.'); setLoading(false); });
  }, []);

  const statusLabel = {
    trial: 'Deneme Süresi',
    active: 'Aktif',
    suspended: 'Askıya Alındı'
  };
  const statusColor = {
    trial: '#f59e0b',
    active: '#16a34a',
    suspended: '#dc2626'
  };

  if (loading) return <div style={{ padding: 60, textAlign: 'center' }}>Yükleniyor...</div>;
  if (error || !data?.success) return (
    <div style={{ padding: 60, textAlign: 'center' }}>
      <p>{error || 'Giriş yapmanız gerekiyor.'}</p>
      <a href="https://test.pekinteknoloji.com/login" style={{ color: '#6366f1' }}>Giriş Yap</a>
    </div>
  );

  const { store, subscription } = data;
  const status = subscription.status;

  return (
    <div style={{ maxWidth: 720, margin: '60px auto', padding: '0 24px', fontFamily: 'sans-serif' }}>
      <h1 style={{ fontSize: 28, fontWeight: 700, marginBottom: 32 }}>Hesabım</h1>

      {/* Mağaza Kartı */}
      <div style={{ background: '#fff', border: '1px solid #e5e7eb', borderRadius: 12, padding: 28, marginBottom: 24, boxShadow: '0 1px 4px rgba(0,0,0,0.06)' }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
          <div>
            <h2 style={{ fontSize: 20, fontWeight: 600, margin: '0 0 4px' }}>{store.name}</h2>
            <a href={store.url} target="_blank" rel="noreferrer" style={{ color: '#6366f1', fontSize: 14 }}>{store.url}</a>
          </div>
          <span style={{ background: statusColor[status] + '20', color: statusColor[status], padding: '4px 12px', borderRadius: 20, fontSize: 13, fontWeight: 600 }}>
            {statusLabel[status] || status}
          </span>
        </div>

        <div style={{ marginTop: 20, display: 'flex', gap: 12 }}>
          <a href={store.adminUrl} target="_blank" rel="noreferrer"
            style={{ background: '#6366f1', color: '#fff', padding: '8px 20px', borderRadius: 8, textDecoration: 'none', fontSize: 14, fontWeight: 500 }}>
            Yönetim Paneli
          </a>
          <a href={store.url} target="_blank" rel="noreferrer"
            style={{ background: '#f3f4f6', color: '#374151', padding: '8px 20px', borderRadius: 8, textDecoration: 'none', fontSize: 14, fontWeight: 500 }}>
            Mağazayı Gör
          </a>
        </div>
      </div>

      {/* Abonelik Kartı */}
      <div style={{ background: '#fff', border: '1px solid #e5e7eb', borderRadius: 12, padding: 28, marginBottom: 24, boxShadow: '0 1px 4px rgba(0,0,0,0.06)' }}>
        <h3 style={{ fontSize: 16, fontWeight: 600, margin: '0 0 16px' }}>Abonelik</h3>
        {status === 'trial' && (
          <div>
            <p style={{ margin: '0 0 8px', color: '#374151' }}>
              Deneme süreniz <b>{subscription.trialEndDate}</b> tarihinde sona eriyor.
            </p>
            <p style={{ margin: '0 0 20px', fontSize: 28, fontWeight: 700, color: subscription.daysRemaining > 3 ? '#16a34a' : '#dc2626' }}>
              {subscription.daysRemaining} gün kaldı
            </p>
            <a href="mailto:bilgi@pekinteknoloji.com?subject=Ücretli%20Plana%20Geçiş"
              style={{ display: 'inline-block', background: '#6366f1', color: '#fff', padding: '10px 24px', borderRadius: 8, fontSize: 15, fontWeight: 600, textDecoration: 'none' }}>
              Ücretli Plana Geç
            </a>
          </div>
        )}
        {status === 'active' && (
          <p style={{ color: '#16a34a', fontWeight: 600 }}>✅ Aktif aboneliğiniz bulunuyor.</p>
        )}
        {status === 'suspended' && (
          <div>
            <p style={{ color: '#dc2626', marginBottom: 16 }}>❌ Deneme süreniz doldu. Mağazanıza erişmek için ücretli plana geçin.</p>
            <a href="mailto:bilgi@pekinteknoloji.com?subject=Mağaza%20Aktifleştirme"
              style={{ display: 'inline-block', background: '#dc2626', color: '#fff', padding: '10px 24px', borderRadius: 8, fontSize: 15, fontWeight: 600, textDecoration: 'none' }}>
              Mağazamı Aktifleştir
            </a>
          </div>
        )}
      </div>

      {/* Fatura Geçmişi */}
      <div style={{ background: '#fff', border: '1px solid #e5e7eb', borderRadius: 12, padding: 28, boxShadow: '0 1px 4px rgba(0,0,0,0.06)' }}>
        <h3 style={{ fontSize: 16, fontWeight: 600, margin: '0 0 16px' }}>Fatura Geçmişi</h3>
        <p style={{ color: '#9ca3af', fontSize: 14 }}>Henüz fatura bulunmuyor.</p>
      </div>
    </div>
  );
};

const MagazaAcPage = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState({ firstName: '', lastName: '', storeName: '', storeSlug: '', email: '', phone: '', password: '' });
  const [loading, setLoading] = useState(false);
  const [step, setStep] = useState(1);
  const { slugStatus, checkSlug } = useSlugCheck();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      const response = await fetch("https://test.pekinteknoloji.com/api/fastregister/register", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(formData),
      });
      const data = await response.json();
      if (data.success) {
        window.location.href = data.autoLoginUrl;
      } else {
        alert("Hata: " + (data.errors ? data.errors.join(", ") : data.message));
        setLoading(false);
      }
    } catch {
      alert("Sunucuya bağlanılamadı.");
      setLoading(false);
    }
  };

  const set = f => e => {
    const value = e.target.value;
    if (f === 'storeName') {
      const slug = toSlug(value);
      setFormData({ ...formData, storeName: value, storeSlug: slug });
      checkSlug(slug);
    } else {
      setFormData({ ...formData, [f]: value });
    }
  };

  const PERKS = [
    { icon: '🚀', text: 'Dakikalar içinde mağazanız hazır' },
    { icon: '🎨', text: 'Premium temalardan birini seçin' },
    { icon: '🔗', text: 'Trendyol & Hepsiburada entegrasyonu' },
    { icon: '🛡️', text: 'SSL sertifikası ve güvenli altyapı' },
    { icon: '📱', text: 'Mobil uyumlu, her cihazda çalışır' },
    { icon: '🎧', text: '7/24 teknik destek' },
  ];

  return (
    <div className="magaza-ac-page">
      {/* Left panel - form */}
      <div className="map-left">
        <div className="map-form-wrap">
          <button className="map-back" onClick={() => navigate('/')}>
            ← Geri Dön
          </button>
          <div className="map-form-header">
            <Logo size="md" />
            <h2>Mağazanızı Oluşturun</h2>
            <p>Kredi kartı gerekmez. Dakikalar içinde hazır.</p>
          </div>

          <form onSubmit={handleSubmit} className="map-form">
            <div className="map-form-group">
              <label>Mağaza Adı</label>
              <input type="text" required placeholder="Örn: Çiçek Butik" value={formData.storeName} onChange={set('storeName')} />
              {formData.storeSlug && (
                <small style={{ display: 'block', marginTop: 4, color: slugStatus === 'available' ? '#16a34a' : slugStatus === 'taken' ? '#dc2626' : '#6b7280' }}>
                  {slugStatus === 'checking' && `🔍 ${formData.storeSlug}.pekinteknoloji.com kontrol ediliyor...`}
                  {slugStatus === 'available' && `✅ ${formData.storeSlug}.pekinteknoloji.com müsait`}
                  {slugStatus === 'taken' && `❌ ${formData.storeSlug}.pekinteknoloji.com kullanılıyor`}
                  {slugStatus === null && `🌐 ${formData.storeSlug}.pekinteknoloji.com`}
                </small>
              )}
            </div>
            <div className="map-form-row">
              <div className="map-form-group">
                <label>Ad</label>
                <input type="text" required placeholder="Adınız" value={formData.firstName} onChange={set('firstName')} />
              </div>
              <div className="map-form-group">
                <label>Soyad</label>
                <input type="text" required placeholder="Soyadınız" value={formData.lastName} onChange={set('lastName')} />
              </div>
            </div>
            <div className="map-form-group">
              <label>E-posta</label>
              <input type="email" required placeholder="ornek@mail.com" value={formData.email} onChange={set('email')} />
            </div>
            <div className="map-form-group">
              <label>Telefon</label>
              <input type="tel" required placeholder="05XX XXX XX XX" value={formData.phone} onChange={e => { const v = formatPhone(e.target.value); setFormData({ ...formData, phone: v }); }} inputMode="numeric" pattern="[0-9 ]{13,14}" />
            </div>
            <div className="map-form-group">
              <label>Şifre</label>
              <input type="password" required placeholder="En az 8 karakter" value={formData.password} onChange={set('password')} />
            </div>

            <button type="submit" className="map-submit-btn" disabled={loading}>
              {loading ? 'Mağaza Oluşturuluyor...' : 'Mağazamı Oluştur'} {!loading && <ArrowRight size={18} />}
            </button>

            <p className="map-terms">
              Devam ederek <a href="mailto:bilgi@pekinteknoloji.com?subject=KVKK%20Bilgi%20Talebi">KVKK</a> ve kullanım koşullarını kabul etmiş olursunuz.
            </p>
          </form>
        </div>
      </div>

      {/* Right panel - perks */}
      <div className="map-right">
        <div className="map-left-inner">
          <h1 className="map-headline">
            Ücretsiz E-Ticaret<br />
            <span className="text-gradient">Mağazanı Aç</span>
          </h1>
          <p className="map-sub">Kurulumdan entegrasyona kadar her adımda yanınızdayız. Hemen satışa başlayın.</p>
          <div className="map-perks">
            {PERKS.map((p, i) => (
              <div key={i} className="map-perk">
                <span className="map-perk-icon">{p.icon}</span>
                <span>{p.text}</span>
              </div>
            ))}
          </div>
          <div className="map-devices">
            <div className="map-desktop-mockup">
              <div className="map-desktop-bar">
                <span className="map-dot" /><span className="map-dot" /><span className="map-dot" />
                <span className="map-url-bar">mağazam.pekinteknoloji.com</span>
              </div>
              <img src={THEMES_DATA[0].mainImage} alt="Masaüstü önizleme" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

const ScrollToTop = () => {
  const { pathname } = useLocation();
  useEffect(() => { window.scrollTo({ top: 0, behavior: 'instant' }); }, [pathname]);
  return null;
};

const BlogDetailPage = () => {
  const { slug } = useParams ? useParams() : {};
  const navigate = useNavigate();
  const post = BLOG_POSTS.find(p => p.slug === slug);
  if (!post) { navigate('/blog'); return null; }
  return (
    <div className="blog-detail-page pt-32 pb-20">
      <div className="container" style={{ maxWidth: 780 }}>
        <button className="btn-ghost mb-8" onClick={() => navigate('/blog')} style={{ display: 'inline-flex', alignItems: 'center', gap: 6, marginBottom: '2rem' }}>
          ← Blog'a Dön
        </button>
        <span className="blog-tag" style={{ background: post.color, position: 'static', marginBottom: '1rem', display: 'inline-block' }}>{post.tag}</span>
        <h1 style={{ fontSize: '2rem', fontWeight: 800, marginBottom: '1rem', lineHeight: 1.3 }}>{post.title}</h1>
        <p style={{ color: '#64748B', marginBottom: '2rem' }}>{post.readTime} okuma</p>
        <img src={post.img} alt={post.title} style={{ width: '100%', borderRadius: 16, marginBottom: '2rem', maxHeight: 360, objectFit: 'cover' }} />
        {post.content ? post.content.map((block, i) => {
          if (block.type === 'h2') return <h2 key={i} style={{ fontSize: '1.35rem', fontWeight: 700, margin: '2rem 0 0.75rem', color: '#1E293B' }}>{block.text}</h2>;
          return <p key={i} style={{ fontSize: '1.05rem', lineHeight: 1.8, color: '#334155', marginBottom: '1rem' }}>{block.text}</p>;
        }) : <p style={{ fontSize: '1.05rem', lineHeight: 1.8, color: '#334155' }}>{post.desc}</p>}
      </div>
    </div>
  );
};

const BlogListPage = () => {
  const navigate = useNavigate();
  return (
    <div className="pt-32 pb-20">
      <div className="container">
        <div className="section-header">
          <span className="text-gradient">Blog</span>
          <h2>E-Ticaret Rehberi</h2>
          <p className="section-desc">Mağazanızı büyütmek için ipuçları ve stratejiler.</p>
        </div>
        <div className="blog-grid">
          {BLOG_POSTS.map((post, i) => (
            <div key={i} className="blog-card" onClick={() => navigate(`/blog/${post.slug}`)} style={{ cursor: 'pointer' }}>
              <div className="blog-card-img">
                <img src={post.img} alt={post.title} />
                <span className="blog-tag" style={{ background: post.color }}>{post.tag}</span>
              </div>
              <div className="blog-card-body">
                <h3>{post.title}</h3>
                <p>{post.desc}</p>
                <div className="blog-card-footer">
                  <span className="blog-read-time">{post.readTime} okuma</span>
                  <span className="blog-read-more" style={{ color: post.color }}>Devamını Oku →</span>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

const FAQPage = () => (
  <div className="pt-32 pb-20">
    <FAQSection />
  </div>
);

function App() {
  const [isRegModalOpen, setRegModalOpen] = useState(false);
  const [isConsultModalOpen, setConsultModalOpen] = useState(false);
  const [selectedTheme, setSelectedTheme] = useState(null);

  useEffect(() => {
    const handleOpen = () => setRegModalOpen(true);
    const handleConsult = () => setConsultModalOpen(true);
    window.addEventListener('open-registration', handleOpen);
    window.addEventListener('open-consult', handleConsult);
    return () => {
      window.removeEventListener('open-registration', handleOpen);
      window.removeEventListener('open-consult', handleConsult);
    };
  }, []);

const AgencyServices = ({ onConsult }) => (
  <section id="services" className="services-section">
    <div className="container">
      <div className="section-header">
        <h2>Ne Yapıyoruz?</h2>
        <p className="section-desc">E-ticaretten kurumsal yazılıma, her ölçekteki işletme için dijital çözümler üretiyoruz.</p>
      </div>
      <div className="services-grid services-grid--3">
        <div className="service-card service-card--featured">
          <div className="service-icon">🛒</div>
          <h3>E-Ticaret Mağazası</h3>
          <p>Kurulum, tema seçimi, ödeme & kargo entegrasyonu, Trendyol & Hepsiburada bağlantısı — her şey dahil. Dakikalar içinde satışa başlayın.</p>
          <ul className="service-list">
            <li>Premium temalar</li>
            <li>Pazaryeri entegrasyonları</li>
            <li>SSL + güvenli ödeme</li>
          </ul>
          <button className="learn-more" onClick={onConsult}>Ücretsiz Danışmanlık →</button>
        </div>
        <div className="service-card">
          <div className="service-icon">💻</div>
          <h3>Kurumsal Yazılım</h3>
          <p>Web sitesi, mobil uygulama veya işletmenize özel yazılım. Fikrinizi ürüne dönüştürüyoruz.</p>
          <ul className="service-list">
            <li>Kurumsal web sitesi</li>
            <li>iOS & Android uygulama</li>
            <li>ERP / CRM çözümleri</li>
          </ul>
          <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
        </div>
        <div className="service-card">
          <div className="service-icon">🛠️</div>
          <h3>Destek & Danışmanlık</h3>
          <p>7/24 teknik destek, düzenli bakım ve dijital dönüşüm danışmanlığı ile yanınızdayız.</p>
          <ul className="service-list">
            <li>7/24 destek</li>
            <li>Proaktif izleme & bakım</li>
            <li>Teknoloji danışmanlığı</li>
          </ul>
          <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
        </div>
      </div>
    </div>
  </section>
);

  const navigate = useNavigate();

  const HomePage = () => (
    <>
      {/* Hero Section - Split */}
      <section className="hero hero-split">
        <motion.div
          initial={{ opacity: 0, x: -30 }}
          animate={{ opacity: 1, x: 0 }}
          className="hero-text"
        >
          <span className="badge">Hazır E-Ticaret Altyapısı</span>
          <h1>E-Ticaret Mağazanızı <br /> <span className="text-gradient">Birlikte Kuralım</span></h1>
          <p className="hero-description">
            Güçlü e-ticaret altyapısıyla mağazanızı kurun.
            Ödeme sistemleri, pazaryeri entegrasyonları ve 7/24 destek tek pakette.
          </p>
          <div className="hero-btns">
            <button className="btn-primary" onClick={() => navigate('/magaza-ac')}>
              Ücretsiz E-Ticaret Siteni Aç <ArrowRight size={20} />
            </button>
          </div>
        </motion.div>
        <motion.div
          initial={{ opacity: 0, x: 30 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ delay: 0.1 }}
          className="hero-visual"
        >
          <div className="browser-mockup">
            <div className="browser-bar">
              <span className="browser-dot red" />
              <span className="browser-dot yellow" />
              <span className="browser-dot green" />
              <span className="browser-url">mağazam.pekinteknoloji.com</span>
            </div>
            <img src={THEMES_DATA[0].mainImage} alt="E-ticaret mağaza örneği" className="browser-screenshot" />
          </div>
        </motion.div>
      </section>

      <IntegrationsStrip />

      <EcommerceTeaser onGoEcommerce={() => navigate('/eticaret')} />

      <AnimatedFeaturesSection />

      <AgencyServices onConsult={() => setConsultModalOpen(true)} />

      <BlogSection onNavigate={(type, slug) => navigate(type === 'blog' ? `/blog/${slug}` : `/${type}`)} />

      <FAQSection />

      {/* Contact Section */}
      <section id="contact" className="contact-section section-padding bg-soft">
        <div className="container">
          <div className="contact-header">
            <h2>Bize Ulaşın</h2>
            <p>Projenizi konuşalım. En geç 1 iş günü içinde dönüş yapıyoruz.</p>
          </div>
          <div className="contact-info-row">
            <a href="tel:+908508402336" className="contact-info-pill">
              <Phone size={16} /> 0850 840 23 36
            </a>
            <a href="mailto:bilgi@pekinteknoloji.com" className="contact-info-pill">
              <Mail size={16} /> bilgi@pekinteknoloji.com
            </a>
          </div>
          <div style={{ borderRadius: 16, overflow: 'hidden', margin: '1.5rem 0', height: 280, boxShadow: '0 2px 16px rgba(0,0,0,0.10)' }}>
            <iframe
              title="Pekin Teknoloji Konum"
              src="https://maps.google.com/maps?q=Selanik+Pasa%C4%B1+No:5+Beyo%C4%9Flu+%C4%B0stanbul&output=embed&hl=tr"
              width="100%"
              height="280"
              style={{ border: 0, display: 'block' }}
              allowFullScreen
              loading="lazy"
              referrerPolicy="no-referrer-when-downgrade"
            />
          </div>
          <ContactInlineForm />
        </div>
      </section>
    </>
  );

  const location = useLocation();
  const isFullscreen = location.pathname === '/magaza-ac';

  return (
    <div className="app">
      <ScrollToTop />
      {!isFullscreen && <Navbar onConsult={() => setConsultModalOpen(true)} />}

      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/eticaret" element={<EcommercePage onRegister={() => navigate('/magaza-ac')} onThemeSelect={setSelectedTheme} themesData={THEMES_DATA} />} />
        <Route path="/fiyatlar" element={<PricingPage />} />
        <Route path="/blog" element={<BlogListPage />} />
        <Route path="/blog/:slug" element={<BlogDetailPage />} />
        <Route path="/sss" element={<FAQPage />} />
        <Route path="/magaza-ac" element={<MagazaAcPage />} />
        <Route path="/hesabim" element={<HesabimPage />} />
        <Route path="*" element={<HomePage />} />
      </Routes>

      {/* Footer */}
      {!isFullscreen && <footer className="footer">
        <div className="footer-inner">
          <div className="footer-top">
            <div className="footer-info">
              <Logo size="md" light />
              <p>Geleceğin teknolojilerini bugünden inşa ediyor, işinizi dijital dünyada zirveye taşıyoruz.</p>
            </div>
            
            <div className="f-col">
              <h4>Hizmetlerimiz</h4>
              <Link to="/" onClick={() => setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 100)}>Mağaza Kurulum</Link>
              <Link to="/" onClick={() => setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 100)}>Tema Özelleştirme</Link>
              <Link to="/" onClick={() => setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 100)}>Pazaryeri Entegrasyonu</Link>
              <Link to="/" onClick={() => setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 100)}>Teknik Destek</Link>
            </div>

            <div className="f-col">
              <h4>Kurumsal</h4>
              <Link to="/">Anasayfa</Link>
              <Link to="/eticaret">E-Ticaret Platform</Link>
              <Link to="/fiyatlar">Fiyatlar</Link>
              <Link to="/blog">Blog</Link>
              <Link to="/sss">SSS</Link>
            </div>

            <div className="f-col">
              <h4>Destek</h4>
              <Link to="/" onClick={() => setTimeout(() => document.getElementById('contact')?.scrollIntoView({ behavior: 'smooth' }), 100)}>İletişim</Link>
              <a href="mailto:bilgi@pekinteknoloji.com">Bize Yazın</a>
              <a href="mailto:bilgi@pekinteknoloji.com?subject=KVKK%20Bilgi%20Talebi">KVKK</a>
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
      </footer>}

      {!isFullscreen && <ConsultationModal
        isOpen={isConsultModalOpen}
        onClose={() => setConsultModalOpen(false)}
      />}

      {!isFullscreen && <RegistrationModal
        isOpen={isRegModalOpen}
        onClose={() => setRegModalOpen(false)}
      />}

      {!isFullscreen && <ThemeModal
        isOpen={!!selectedTheme}
        theme={selectedTheme}
        onClose={() => setSelectedTheme(null)}
      />}
    </div>
  );
}

export default App;
