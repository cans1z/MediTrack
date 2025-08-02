'use client'
import { useEffect } from 'react';
import styles from './HomeScreen.module.css';
import useAuth from '@/hooks/useAuth';
import { useRouter } from 'next/navigation';

const HomeScreen = () => {
  const auth = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!auth?.loading && !auth?.user) {
      router.push('login');
    }
  }, [auth]);

  return (
    <div>

    </div>
  );
}

export default HomeScreen;