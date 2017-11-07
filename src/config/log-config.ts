// const rln = /\[(.*)\]/
// const rkv = /\]\s([\s\S]*)$/
import { Configuration } from 'log4js'

const createConfig = function (basePath: string): Configuration {
  return {
    appenders: {
      debug: {
        type: 'console',
        layout: {
          type: 'pattern',
          pattern: '[%r] [%[%5.5p%]] - %m%n'
        },
      },
      access: {
        type: 'dateFile',
        fileName: `${basePath}/access`,
        category: 'access',
      },
      info: {
        type: 'dateFile',
        fileName: `${basePath}/info`,
        category: 'info'
      },
      error: {
        type: 'dateFile',
        fileName: `${basePath}/error`,
        category: 'error'
      }
    },
    categories: {
      debug: { appenders: ['debug'], level: 'debug' },
      access: { appenders: ['access'], level: 'all' },
      info: { appenders: ['access'], level: 'info' },
      error: { appenders: ['access'], level: 'error' },
    }
  }
  /**
  if (options.env === 'development') {
    return {
      appenders: [
        {
          type: 'console',
          layout: {
            type: 'pattern',
            pattern: '%[[%p]%] %x{x}',
            tokens: {
              x(e) {
                const ln = e.data[0].match(rln)[1]
                const kv = e.data[0].match(rkv)[1]
                let str
                if (e.categoryName === 'log_request') {
                  str = kv.split('||').map((entry, index) => {
                    let [key, value = ''] = entry.split('=')
                    return index === 0 ? `\x1B[1;37;${key === '_manhattan_http_success' ? 42 : 41}m ${key} \x1B[0m` : `\x1B[1m${key}\x1B[0m\t${value}`
                  }).join('\n') + '\n'
                }
                else {
                  return `\x1B[1m${ln}\x1B[0m ${kv}\n`
                }
                return str
              }
            }
          }
        }
      ],
      levels: {
        '[all]': 'WARN',
        'log_request': 'INFO'
      }
    }
  }
  else {
    const makeAppenders = (type: string) => {
      return {
        category: `log_${type}`,
        type: 'dateFile',
        filename: `${options.path}/log_${type}/${type}.log`,
        layout: {
          type: 'pattern',
          pattern: '[%p][%d{ISO8601_WITH_TZ_OFFSET}]%m',
        },
        alwaysIncludePattern: true
      }
    }
    return {
      appenders: ['info', 'error', 'access', 'request', 'sql'].map(makeAppenders),
      levels: {
        '[all]': 'INFO'
      }
    }
  }
  */
}

export default createConfig
