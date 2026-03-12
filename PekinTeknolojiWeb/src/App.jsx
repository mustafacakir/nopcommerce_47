import React, { useState, useEffect } from 'react';
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
  Check
} from 'lucide-react';
import { motion, AnimatePresence } from 'framer-motion';
import './App.css';

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
  const items = ['Trendyol', 'Hepsiburada', 'n11', 'Amazon', 'Çiçeksepeti', 'iyzico', 'PayTR', 'Garanti BBVA', 'MNG Kargo', 'Yurtiçi Kargo', 'Aras Kargo', 'Sürat Kargo'];
  return (
    <div className="integrations-strip">
      <div className="container">
        <p className="integrations-label">Türkiye'nin önde gelen platformlarıyla entegre</p>
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
        <div className="fv-phone">
          <div className="fv-phone-bar" />
          <div className="fv-phone-content">
            <div className="fv-product-img" />
            <div className="fv-product-name" />
            <div className="fv-product-price" />
            <div className="fv-add-cart">Sepete Ekle</div>
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
        <div className="fv-mp-wrap">
          <div className="fv-mp-hub">Mağaza</div>
          {['Trendyol', 'Hepsiburada', 'n11', 'Amazon'].map((name, i) => (
            <div key={name} className={`fv-mp-node fv-node-${i}`}>{name}</div>
          ))}
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
        <h2>{THEMES_DATA.length} Premium Tema</h2>
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
  { tag: 'SEO', title: 'E-Ticarette SEO: Mağazanızı Google\'da Öne Çıkarın', desc: 'Doğru URL yapısı, meta etiketler ve içerik stratejisiyle organik trafiğinizi katlayın.', readTime: '5 dk', color: '#2563EB' },
  { tag: 'Pazaryeri', title: 'Trendyol Entegrasyonu ile Satışlarınızı Artırın', desc: 'Ürünlerinizi Trendyol ile senkronize edin, stok ve siparişleri tek panelden yönetin.', readTime: '4 dk', color: '#F97316' },
  { tag: 'Tasarım', title: 'E-Ticaret Teması Seçerken Dikkat Edilmesi Gerekenler', desc: 'Dönüşüm oranını etkileyen tasarım kararları ve doğru tema seçim kriterleri.', readTime: '6 dk', color: '#8B5CF6' },
];

const BlogSection = () => (
  <section className="blog-section section-padding">
    <div className="container">
      <div className="section-header">
        <span className="text-gradient">Blog</span>
        <h2>E-Ticaret Rehberi</h2>
        <p className="section-desc">Mağazanızı büyütmek için ipuçları ve stratejiler.</p>
      </div>
      <div className="blog-grid">
        {BLOG_POSTS.map((post, i) => (
          <div key={i} className="blog-card">
            <div className="blog-card-top" style={{ background: `linear-gradient(135deg, ${post.color}22, ${post.color}08)` }}>
              <span className="blog-tag" style={{ background: post.color }}>{post.tag}</span>
            </div>
            <div className="blog-card-body">
              <h3>{post.title}</h3>
              <p>{post.desc}</p>
              <span className="blog-read-time">{post.readTime} okuma</span>
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
  { q: 'Kaç farklı tema var, özelleştirebilir miyim?', a: '27 premium tema arasından seçim yapabilirsiniz. Renk, font ve layout tamamen markanıza göre özelleştirilebilir.' },
  { q: 'Teknik destek ne zaman ulaşılabilir?', a: '7/24 Türkçe teknik destek ekibimiz her an yanınızda. Acil durumlarda maksimum 2 saat içinde müdahale garantisi veriyoruz.' },
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
          <a href="#" onClick={(e) => {
            e.preventDefault();
            if (currentPage !== 'home') {
              onPageChange('home');
              setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 150);
            } else {
              document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' });
            }
          }}>Hizmetler</a>
          <a href="#" onClick={(e) => {
            e.preventDefault();
            if (currentPage !== 'home') {
              onPageChange('home');
              setTimeout(() => document.getElementById('contact')?.scrollIntoView({ behavior: 'smooth' }), 150);
            } else {
              document.getElementById('contact')?.scrollIntoView({ behavior: 'smooth' });
            }
          }}>İletişim</a>
          <div className="nav-cta-group">
            <button className="btn-nav-secondary" onClick={() => onPageChange('pricing')}>Fiyatlar</button>
            <button className="btn-nav" onClick={onConsult}>Danışmanlık Al</button>
          </div>
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
      await fetch('http://localhost:8085/api/fastregister/consultation', {
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
    <div className="contact-form-col">
      {sent ? (
        <div className="contact-form-success">
          <CheckCircle2 size={40} className="success-icon" />
          <h3>Mesajınız Alındı</h3>
          <p>En geç 1 iş günü içinde size dönüş yapıyoruz.</p>
        </div>
      ) : (
        <form onSubmit={handleSubmit} className="contact-inline-form">
          <h3 className="contact-form-title">Mesaj Gönderin</h3>
          <div className="form-group">
            <input type="text" required placeholder="Adınız Soyadınız" value={formData.name} onChange={set('name')} />
          </div>
          <div className="form-group">
            <input type="email" required placeholder="E-posta adresiniz" value={formData.email} onChange={set('email')} />
          </div>
          <div className="form-group">
            <input type="tel" placeholder="Telefon (opsiyonel)" value={formData.phone} onChange={set('phone')} />
          </div>
          <div className="form-group">
            <textarea required rows={4} placeholder="Mesajınız..." value={formData.message} onChange={set('message')} />
          </div>
          <button type="submit" className="btn-primary btn-full" disabled={loading}>
            {loading ? 'Gönderiliyor...' : 'Gönder'} {!loading && <ArrowRight size={16} />}
          </button>
          <p className="contact-form-note">En geç 1 iş günü içinde dönüş yapıyoruz.</p>
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
    storeName: '',
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
            <div className="form-group">
              <label>Mağaza Adı</label>
              <input type="text" required placeholder="Mağazanızın adı" value={formData.storeName} onChange={e => setFormData({...formData, storeName: e.target.value})} />
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
      { text: 'Tüm 27 premium tema', ok: true },
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
  { icon: <Cpu size={22}/>, title: 'Akıllı Entegrasyonlar', desc: 'Trendyol, Hepsiburada, n11 ve kargo firmalarıyla tek tıkla bağlanın.', stat: '8+', statLabel: 'Entegrasyon' },
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
          <h2>Markanıza özel 27 premium tema</h2>
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

function App() {
  const [isRegModalOpen, setRegModalOpen] = useState(false);
  const [isConsultModalOpen, setConsultModalOpen] = useState(false);
  const [selectedTheme, setSelectedTheme] = useState(null);
  const [currentPage, setCurrentPage] = useState('home');

  useEffect(() => {
    const handleOpen = () => setRegModalOpen(true);
    const handleConsult = () => setConsultModalOpen(true);
    window.addEventListener('open-registration', handleOpen);
    window.addEventListener('open-consult', handleConsult);
    window.scrollTo({ top: 0, behavior: 'instant' });
    return () => {
      window.removeEventListener('open-registration', handleOpen);
      window.removeEventListener('open-consult', handleConsult);
    };
  }, [currentPage]);

const AgencyServices = ({ onConsult }) => (
  <>
    <section id="services" className="services-section">
      <div className="container">
        <div className="section-header">
          <h2>Yazılım & Dijital Hizmetler</h2>
          <p className="section-desc">İşinizi büyütmek için ihtiyacınız olan tüm dijital çözümler tek çatı altında.</p>
        </div>
        <div className="services-grid">
          <div className="service-card">
            <div className="service-icon">🌐</div>
            <h3>Kurumsal Web Sitesi</h3>
            <p>Markanızı en iyi şekilde yansıtan, hızlı ve SEO uyumlu kurumsal web siteleri tasarlıyor ve geliştiriyoruz.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
          <div className="service-card">
            <div className="service-icon">📱</div>
            <h3>Mobil Uygulama</h3>
            <p>iOS ve Android için yerli, kullanıcı dostu mobil uygulamalar geliştiriyoruz. Fikrden mağazaya kadar her adımda yanınızdayız.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
          <div className="service-card">
            <div className="service-icon">💻</div>
            <h3>Özel Yazılım Geliştirme</h3>
            <p>İşletmenize özel ERP, CRM veya iş akışı yazılımları. Hazır çözümlerin yetmediği yerde devreye giriyoruz.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
          <div className="service-card">
            <div className="service-icon">🎯</div>
            <h3>Teknoloji Danışmanlığı</h3>
            <p>Doğru teknolojiyi seçmek, sisteminizi ölçeklendirmek veya dijital dönüşümü planlamak için uzman desteği alın.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
        </div>
      </div>
    </section>

    <section id="ecommerce-services" className="services-section services-section--alt">
      <div className="container">
        <div className="section-header">
          <h2>E-Ticaret Hizmetlerimiz</h2>
          <p className="section-desc">Kurulumdan entegrasyona, temadan teknik desteğe kadar her şey dahil.</p>
        </div>
        <div className="services-grid">
          <div className="service-card">
            <div className="service-icon">🚀</div>
            <h3>Mağaza Kurulum & Ayarlama</h3>
            <p>Güçlü e-ticaret altyapısıyla mağazanızı hızlıca kurun. Domain, SSL, ödeme sistemi ve kargo entegrasyonları dahil.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
          <div className="service-card">
            <div className="service-icon">🎨</div>
            <h3>Tema Kurulum & Özelleştirme</h3>
            <p>27 premium tema arasından seçin veya markanıza özel tasarım yaptırın. Renk, font ve layout tamamen size göre.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
          <div className="service-card">
            <div className="service-icon">🔗</div>
            <h3>Pazaryeri Entegrasyonu</h3>
            <p>Trendyol, Hepsiburada, n11 ve Amazon entegrasyonlarıyla tüm kanallarınızı tek panelden yönetin.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
          <div className="service-card">
            <div className="service-icon">🛠️</div>
            <h3>Teknik Destek & Bakım</h3>
            <p>7/24 Türkçe destek, düzenli güncellemeler ve proaktif izleme ile mağazanız her zaman çalışır durumda.</p>
            <button className="learn-more" onClick={onConsult}>Teklif Al →</button>
          </div>
        </div>
      </div>
    </section>
  </>
);

  const HomePage = () => (
    <>
      {/* Hero Section - Split */}
      <section className="hero hero-split">
        <motion.div
          initial={{ opacity: 0, x: -30 }}
          animate={{ opacity: 1, x: 0 }}
          className="hero-text"
        >
          <span className="badge">Türkiye'ye Hazır E-Ticaret Altyapısı</span>
          <h1>E-Ticaret Mağazanızı <br /> <span className="text-gradient">Birlikte Kuralım</span></h1>
          <p className="hero-description">
            Güçlü e-ticaret altyapısıyla Türkiye'ye hazır mağazanızı kurun.
            Yerli ödeme sistemleri, pazaryeri entegrasyonları ve 7/24 Türkçe destek tek pakette.
          </p>
          <div className="hero-btns">
            <button className="btn-primary" onClick={() => setConsultModalOpen(true)}>
              Ücretsiz Danışmanlık Al <ArrowRight size={20} />
            </button>
            <button className="btn-ghost" onClick={() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' })}>
              Hizmetlerimizi Keşfedin
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

      <AgencyServices onConsult={() => setConsultModalOpen(true)} />

      <AnimatedFeaturesSection />

      <EcommerceTeaser onGoEcommerce={() => setCurrentPage('ecommerce')} />

      <BlogSection />

      <FAQSection />

      {/* Contact Section */}
      <section id="contact" className="section-padding bg-soft">
        <div className="container">
          <div className="section-header">
            <h2>Bize Ulaşın</h2>
            <p className="section-desc">Projenizi konuşalım. Ücretsiz danışmanlık için formu doldurun veya doğrudan iletişime geçin.</p>
          </div>

          <div className="contact-layout">
            <div className="contact-info-col">
              <a href="tel:+908508402336" className="contact-info-card">
                <Phone size={24} />
                <div>
                  <span className="contact-info-label">Telefon</span>
                  <span className="contact-info-value">0850 840 23 36</span>
                </div>
              </a>
              <a href="mailto:bilgi@pekinteknoloji.com" className="contact-info-card">
                <Mail size={24} />
                <div>
                  <span className="contact-info-label">E-Posta</span>
                  <span className="contact-info-value">bilgi@pekinteknoloji.com</span>
                </div>
              </a>
              <div className="contact-info-card no-link">
                <MapPin size={24} />
                <div>
                  <span className="contact-info-label">Adres</span>
                  <span className="contact-info-value">Selanik Pasajı No:5, Beyoğlu/İstanbul</span>
                </div>
              </div>
              <MapSection />
            </div>

            <ContactInlineForm />
          </div>
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
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('home'); setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>Mağaza Kurulum</a>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('home'); setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>Tema Özelleştirme</a>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('home'); setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>Pazaryeri Entegrasyonu</a>
              <a href="#" onClick={(e) => { e.preventDefault(); setCurrentPage('home'); setTimeout(() => document.getElementById('services')?.scrollIntoView({ behavior: 'smooth' }), 150); }}>Teknik Destek</a>
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
